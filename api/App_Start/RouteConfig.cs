using System.Web.Mvc;
using System.Web.Routing;

namespace API
{
    /// <summary>
    /// Route config.
    /// </summary>
    public static class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">Routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}/{route}/{opcional}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    route = UrlParameter.Optional,
                    opcional = UrlParameter.Optional
                }
            );
        }
    }
}
