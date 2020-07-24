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
    public class LogController : ApiController
    {
        /// <summary>
        /// List the specified searchParams.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="searchParams">Search parameters.</param>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/logs")]
        public async Task<object> List([FromUri]SearchParams searchParams)
        {
            return Ok(await new LogService().List(searchParams));
        }
    }
}
