using System.Web.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Restricted controller.
    /// </summary>
    public class RestrictedController : Controller
    {
        /// <summary>
        /// Index this instance.
        /// </summary>
        /// <returns>The index.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
