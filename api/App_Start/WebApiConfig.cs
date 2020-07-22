using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using API.Filters;

namespace API
{
    /// <summary>
    /// Web API config.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register the specified config.
        /// </summary>
        /// <param name="config">Config.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            EnableCorsAttribute corsAttr = new EnableCorsAttribute(WebConfigurationManager.AppSettings["CorsHost"], "*", "*", "*");
            config.EnableCors(corsAttr);

            config.Filters.Add(new AuthenticationFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
