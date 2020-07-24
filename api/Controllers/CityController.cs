using System.Threading.Tasks;
using System.Web.Http;
using API.Models;
using API.Services;

namespace API.Controllers
{
    /// <summary>
    /// State controller.
    /// </summary>
    [Authorize]
    public class CityController : ApiController
    {
        /// <summary>
        /// List the specified searchParams.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="searchParams">Search parameters.</param>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/cities")]
        public async Task<object> List([FromUri]SearchParams searchParams)
        {
            return Ok(await new CityService().List(searchParams));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/cities/:stateId")]
        public async Task<object> List([FromUri]int stateId)
        {
            return Ok(await new CityService().ByStateId(stateId));
        }
    }
}
