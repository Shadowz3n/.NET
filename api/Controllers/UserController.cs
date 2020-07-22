using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
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

        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/register")]
        public object Register(User user)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            if (WebConfigurationManager.AppSettings["GoogleRecaptcha"] == "true")
            {
                
            }

            return Ok(new { result =  user });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/user")]
        public object List([FromUri]SearchParams searchParams)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            if (WebConfigurationManager.AppSettings["GoogleRecaptcha"] == "true")
            {

            }

            return Ok(new { result = searchParams });
        }

        [HttpPost]
        [Route("api/user")]
        public object Add()
        {
            return Ok(new { });
        }

        [HttpPut]
        [Route("api/user")]
        public object Edit()
        {
            return Ok(new { });
        }

        [HttpDelete]
        [Route("api/user")]
        public object Delete()
        {
            return Ok(new { });
        }
    }
}
