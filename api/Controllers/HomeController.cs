using System.Web.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
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
