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
    public class StateController : ApiController
    {
        private StateService _stateService = new StateService();

        /// <summary>
        /// List the specified searchParams.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="searchParams">Search parameters.</param>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/states")]
        public async Task<object> List([FromUri]SearchParams searchParams)
        {
            return Ok(await _stateService.List(searchParams));
        }

        /// <summary>
        /// Add or edit the specified state.
        /// </summary>
        /// <returns>Add and Edit.</returns>
        /// <param name="state">State.</param>
        [HttpPost]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("api/state")]
        public async Task<object> AddAndEdit([FromBody]State state)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            return Ok(await _stateService.AddAndEdit(state));
        }

        /// <summary>
        /// Delete the specified id.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("api/state")]
        public async Task<object> Delete([FromBody]int id)
        {
            return Ok(await _stateService.Delete(id));
        }
    }
}
