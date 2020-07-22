using System.Web.Mvc;
using System.Web.Security;

namespace API.Utils.Helper
{
    public class APISession : Controller
    {
        public bool Check(string credentials)
        {
            if (System.Web.HttpContext.Current != null
                && System.Web.HttpContext.Current.User != null
                && System.Web.HttpContext.Current.User.Identity.IsAuthenticated
                && System.Web.HttpContext.Current.User.Identity.Name != null
                && credentials.Contains(System.Web.HttpContext.Current.User.Identity.Name))
            {
                return true;
            }

            return false;
        }

        public object Get()
        {
            //System.Web.HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            return Request.Cookies;
        }

        public void Set(string credential)
        {
            FormsAuthentication.SetAuthCookie(credential, false);
        }

        public void Destroy()
        {
            FormsAuthentication.SignOut();
        }
    }
}
