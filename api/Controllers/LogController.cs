using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models;
using API.Services;

namespace API.Controllers
{
    /// <summary>
    /// Log controller.
    /// </summary>
    [Authorize]
    public class LogController : ApiController
    {
        /// <summary>
        /// List the specified searchParams.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="searchParams">Search parameters.</param>
        [HttpGet]
        [Route("api/logs")]
        [Authorize(Roles = "Admin")]
        public async Task<object> List([FromUri]SearchParams searchParams)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            return Ok(await new LogService().List(searchParams));
        }
    }
}
