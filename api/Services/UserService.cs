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
                            }). Take(1).ToArrayAsync();

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
            await new LogHelper().Save(log);

            return new { tokenType, accessToken };
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
