using System.Web.Mvc;

namespace API.Controllers
{
    public class RestrictedController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
