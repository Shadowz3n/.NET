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
        private CityService _cityService = new CityService();

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
            return Ok(await _cityService.List(searchParams));
        }

        /// <summary>
        /// List cities the specified stateId.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="stateId">State identifier.</param>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/cities/:stateId")]
        public async Task<object> List([FromUri]int stateId)
        {
            return Ok(await _cityService.ByStateId(stateId));
        }
    }
}
