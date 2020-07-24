using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [Authorize]
    public class UserController : ApiController
    {
        // POST: api/user/login
        /// <summary>
        /// Returns user login response
        /// </summary>
        /// <returns>The login response.</returns>
        /// <param name="userLogin">User login.</param>
        /// <response code="200">{ tokenType: string, accessToken: string }</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/login")]
        public object Login([FromBody]UserLogin userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            if (WebConfigurationManager.AppSettings["GoogleRecaptcha"] == "true")
            {

            }

            return Ok(new { result = userLogin });
        }

        // POST: api/user/register
        /// <summary>
        /// Register the specified user.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="user">User.</param>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/register")]
        public object Register([FromBody]User user)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            if (WebConfigurationManager.AppSettings["GoogleRecaptcha"] == "true")
            {
                
            }

            return Ok(new { result =  user });
        }

        // GET: api/users
        /// <summary>
        /// List the specified searchParams.
        /// </summary>
        /// <returns>The user list.</returns>
        /// <param name="searchParams">Search parameters.</param>
        [HttpGet]
        [Route("api/users")]
        [Authorize(Roles = "Admin")]
        public object List([FromUri]SearchParams searchParams)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            return Ok(new { result = searchParams });
        }

        // POST: api/user
        /// <summary>
        /// Add user in Admin
        /// </summary>
        /// <returns>The added user.</returns>
        [HttpPost]
        [Route("api/user")]
        [Authorize(Roles = "Admin")]
        public object Add()
        {
            return Ok(new { });
        }

        // PUT: api/user
        /// <summary>
        /// Edit user in admin.
        /// </summary>
        /// <returns>The edited user.</returns>
        [HttpPut]
        [Route("api/user")]
        [Authorize(Roles = "Admin")]
        public object Edit()
        {
            return Ok(new { });
        }

        // DELETE: api/user/{id:int}
        /// <summary>
        /// Delete the specified user id.
        /// </summary>
        /// <returns>The id.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete]
        [Route("api/user/{id:int}")]
        [Authorize(Roles = "Admin")]
        public object Delete(int id)
        {
            return Ok(new { id });
        }
    }
}
