using System.Web.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// State controller.
    /// </summary>
    public class StateController : Controller
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
