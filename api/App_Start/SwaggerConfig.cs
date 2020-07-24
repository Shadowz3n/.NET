using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using Swashbuckle.Application;

namespace API.App_Start
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.MultipleApiVersions(
                        (apiDesc, version) =>
                        {
                            var path = apiDesc.RelativePath.Split('/');
                            var pathVersion = path[1];
                            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(pathVersion, version, CompareOptions.IgnoreCase) >= 0;
                        },
                        vc =>
                        {
                            vc.Version("v2", "API info")
                               .Description("A sample API for testing and prototyping Swashbuckle features")
                               .TermsOfService("Some terms")
                               .Contact(cc => cc
                                    .Name("Some contact")
                                    .Url("http://tempuri.org/contact")
                                    .Email("api@careeronestop.org"))
                                .License(lc => lc
                                    .Name("Some License")
                                    .Url("http://tempuri.org/license"));

                            vc.Version("v1", "API info");
                        });

                    c.ApiKey("apiKey")
                        .Description("API Key Authentication")
                        .Name("apiKey")
                        .In("header");

                    c.IncludeXmlComments(GetXmlCommentsPath());

                    //c.OperationFilter<AddDefaultResponse>();

                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                })
                .EnableSwaggerUi(c =>
                {

                });
        }

        private static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\SwashbuckleWebApiDemo.XML", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
