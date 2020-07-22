using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Routing;
using API.Controllers;

namespace API
{
    /// <summary>
    /// Global.
    /// </summary>
    public class Global : HttpApplication
    {
        /// <summary>
        /// Applications the error.
        /// </summary>
        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            HttpException httpException = exception as HttpException;

            if (httpException.GetHttpCode() == 403 || httpException.GetHttpCode() == 500)
            {
                Response.Redirect(WebConfigurationManager.AppSettings["NotFoundPage"]);
                return;
            }

            if (httpException.GetHttpCode() == 404)
            {
                Server.ClearError();
                RouteData routeData = new RouteData();
                HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
                UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                RouteData thisRouteData = urlHelper.RouteCollection.GetRouteData(currentContext);
                string thisController = thisRouteData.Values["controller"] as string;
                if (thisController.ToLower() == "restricted")
                {
                    routeData.Values["controller"] = "Restrited";
                    routeData.Values["action"] = "Index";
                    IController controller = new RestrictedController();
                    RequestContext rc = new RequestContext(new HttpContextWrapper(Context), routeData);
                    controller.Execute(rc);
                }
                else
                {
                    routeData.Values["controller"] = "Home";
                    routeData.Values["action"] = "Index";
                    IController controller = new HomeController();
                    RequestContext rc = new RequestContext(new HttpContextWrapper(Context), routeData);
                    controller.Execute(rc);
                }
            }
        }

        /// <summary>
        /// Applications the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            EnableCorsAttribute corsAttr = new EnableCorsAttribute(WebConfigurationManager.AppSettings["CorsHost"], "*", "*", "*");
            GlobalConfiguration.Configuration.EnableCors(corsAttr);
        }
    }
}
