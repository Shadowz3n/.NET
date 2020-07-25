using System.Linq;
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

        /// <summary>
        /// Add or edit the specified city.
        /// </summary>
        /// <returns>Add and edit.</returns>
        /// <param name="city">City.</param>
        [HttpPost]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("api/city")]
        public async Task<object> AddAndEdit([FromBody]City city)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            return Ok(await _cityService.AddAndEdit(city));
        }

        /// <summary>
        /// Delete the specified id.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("api/city")]
        public async Task<object> Delete([FromBody]int id)
        {
            return Ok(await _cityService.Delete(id));
        }
    }
}
