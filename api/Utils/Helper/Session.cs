using System.Web.Mvc;
using System.Web.Security;

namespace API.Utils.Helper
{
    /// <summary>
    /// API Session.
    /// </summary>
    public class APISession : Controller
    {
        /// <summary>
        /// Check the specified credentials.
        /// </summary>
        /// <returns>The check.</returns>
        /// <param name="credentials">Credentials.</param>
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

        /// <summary>
        /// Get session.
        /// </summary>
        /// <returns>The get.</returns>
        public object Get()
        {
            //System.Web.HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            return Request.Cookies;
        }

        /// <summary>
        /// Set session.
        /// </summary>
        /// <param name="credential">Credential.</param>
        public void Set(string credential)
        {
            FormsAuthentication.SetAuthCookie(credential, false);
        }

        /// <summary>
        /// Destroy session.
        /// </summary>
        public void Destroy()
        {
            FormsAuthentication.SignOut();
        }
    }
}
