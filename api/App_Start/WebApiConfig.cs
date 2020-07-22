﻿using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using API.Filters;

namespace API
{
    public static class WebApiConfig
    {
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
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
