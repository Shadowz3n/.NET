using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DAL;
using API.Models;
using API.Utils.Helper;

namespace API.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : DbContext
    {
        private APIContext db = new APIContext();
        private HashPassword _hashPassword = new HashPassword();
        private TokenManager _tokenManager = new TokenManager();

        /// <summary>
        /// List the specified searchParams.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="searchParams">Search parameters.</param>
        public async Task<object> List(SearchParams searchParams)
        {
            User[] Results = await (from e in db.Users where e.DeletedAt == null where e.Email.Contains(searchParams.Search) select e)
                                .OrderBy(searchParams.LinqOrder)
                                .Skip(searchParams.OffSet)
                                .Take(searchParams.Limit).ToArrayAsync();

            int Total = await (from e in db.Users where e.DeletedAt == null where e.Email.Contains(searchParams.Search) select e).CountAsync();
            return new { Total, Results };
        }

        /// <summary>
        /// Auth the specified user.
        /// </summary>
        /// <returns>The auth.</returns>
        /// <param name="userLogin">User login.</param>
        public async Task<object> Auth(UserLogin userLogin)
        {
            string passwordHash = _hashPassword.Generate(userLogin.Password);

            var user = await (from u in db.Users
                            join r in db.UserRoles on u.RoleID equals r.ID
                            where u.Email == userLogin.Email
                            where u.Password == passwordHash
                            where u.DeletedAt == null
                            select new {
                               u.ID,
                               u.Name,
                               u.Email,
                               r.Role
                            }).Take(1).ToArrayAsync();

            if (!user.Any())
                return null;

            Claim[] claims = {
                new Claim(ClaimTypes.Name, user.FirstOrDefault().Name),
                new Claim(ClaimTypes.Email, user.FirstOrDefault().Email),
                new Claim(ClaimTypes.Role, user.FirstOrDefault().Role)
            };

            string tokenType = "Bearer";
            string accessToken = _tokenManager.Generate(claims);

            // Save Log
            Log log = new Log
            {
                UserID = user.FirstOrDefault().ID,
                Action = "user.login"
            };
            await new LogService().Save(log);

            return new { tokenType, accessToken };
        }

        /// <summary>
        /// Register the specified userRegister.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="userRegister">User register.</param>
        public async Task<UserRegisterResponse> Register(UserRegister userRegister)
        {
            // Register response
            UserRegisterResponse userRegisterResponse = new UserRegisterResponse();

            // Check if user email exists
            User[] checkUserEmail = await (from u in db.Users
                                    where u.Email == userRegister.Email
                                    select u).Take(1).ToArrayAsync();

            if (checkUserEmail.Any())
            {
                userRegisterResponse.ErrorEmail = true;
                return userRegisterResponse;
            }

            // Check if user cpf exists
            User[] checkUserCpf = await (from u in db.Users
                                        where u.Cpf == userRegister.Cpf
                                        select u).Take(1).ToArrayAsync();

            if (checkUserCpf.Any())
            {
                userRegisterResponse.ErrorCpf = true;
                return userRegisterResponse;
            }


            // Default Normal User Role ID
            int roleId = 1;

            // Save User
            User user = new User
            {
                Name = userRegister.Name,
                Lastname = userRegister.Lastname,
                Email = userRegister.Email,
                AcceptReleases = userRegister.AcceptReleases,
                Password = new HashPassword().Generate(userRegister.Password),
                StateID = userRegister.StateID,
                CityID = userRegister.CityID,
                Cpf = userRegister.Cpf,
                Cnpj = userRegister.Cnpj,
                RoleID = roleId,
                CreatedAt = DateTime.Now
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();

            // Save Log
            Log log = new Log
            {
                UserID = user.ID,
                Action = "user.register"
            };
            await new LogService().Save(log);

            // User Token
            Claim[] claims = {
                new Claim(ClaimTypes.Name, userRegister.Name),
                new Claim(ClaimTypes.Email, userRegister.Email),
                new Claim(ClaimTypes.Role, roleId.ToString())
            };

            userRegisterResponse.TokenType = "Bearer";
            userRegisterResponse.AccessToken = _tokenManager.Generate(claims);

            return userRegisterResponse;
        }

        /// <summary>
        /// Add the specified user.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="user">User.</param>
        public async Task<UserAddResponse> Add(User user)
        {
            // Add response
            UserAddResponse userAddResponse = new UserAddResponse();

            // Check if user email or cpf exists
            User[] checkUser = await (from u in db.Users
                                      where (u.Email == user.Email || u.Cpf == user.Cpf)
                                      where u.ID != user.ID
                                      select u).Take(1).ToArrayAsync();

            if (checkUser.Any())
            {
                if (checkUser.FirstOrDefault().Email == user.Email)
                    userAddResponse.ErrorEmail = true;

                if (checkUser.FirstOrDefault().Cpf == user.Cpf)
                    userAddResponse.ErrorCpf = true;

                return userAddResponse;
            }

            user.Password = new HashPassword().Generate(user.Password);
            user.CreatedAt = DateTime.Now;
            db.Users.Add(user);

            // Save Log
            Log log = new Log
            {
                UserID = user.ID,
                Action = "user.add"
            };
            await new LogService().Save(log);

            Claim[] claims = {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            userAddResponse.TokenType = "Bearer";
            userAddResponse.AccessToken = _tokenManager.Generate(claims);

            return userAddResponse;
        }

        /// <summary>
        /// Edit the specified user.
        /// </summary>
        /// <returns>The edit.</returns>
        /// <param name="user">User.</param>
        public async Task<UserEditResponse> Edit(User user)
        {
            // Edit response
            UserEditResponse userEditResponse = new UserEditResponse();

            // Check ID
            int id = int.TryParse(user.ID.ToString(), out id) ? id : 0;
            if(id == 0)
            {
                userEditResponse.ErrorId = true;
                return userEditResponse;
            }

            // Check if user email or cpf exists
            User[] checkUser = await (from u in db.Users
                                      where (u.Email == user.Email || u.Cpf == user.Cpf)
                                      where u.ID != user.ID
                                      select u).Take(1).ToArrayAsync();

            if (checkUser.Any())
            {
                if(checkUser.FirstOrDefault().Email == user.Email)
                    userEditResponse.ErrorEmail = true;

                if (checkUser.FirstOrDefault().Cpf == user.Cpf)
                    userEditResponse.ErrorCpf = true;

                return userEditResponse;
            }

            user.Password = new HashPassword().Generate(user.Password);
            user.UpdatedAt = DateTime.Now;
            db.Users.Add(user);

            // Save Log
            Log log = new Log
            {
                UserID = user.ID,
                Action = "user.edit"
            };
            await new LogService().Save(log);

            Claim[] claims = {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            userEditResponse.TokenType = "Bearer";
            userEditResponse.AccessToken = _tokenManager.Generate(claims);

            return userEditResponse;
        }

        /// <summary>
        /// Delete the specified id.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<bool> Delete(int id)
        {
            // Check if user id exists
            User[] user = await (from u in db.Users
                                 where u.ID == id
                                 select u).Take(1).ToArrayAsync();

            if (!user.Any())
                return false;

            // Save Log
            Log log = new Log
            {
                UserID = id,
                Action = "user.delete"
            };
            await new LogService().Save(log);

            user.FirstOrDefault().DeletedAt = DateTime.Now;
            await db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Dispose the specified disposing.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
