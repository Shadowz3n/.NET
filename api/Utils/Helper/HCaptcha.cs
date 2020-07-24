using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace API.Utils.Helper
{
    public class HCaptcha
    {
        static HttpClient client = new HttpClient();

        public async Task<HttpResponseMessage> ValidateHCaptcha(string token)
        {

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", WebConfigurationManager.AppSettings["HCaptchaTokenApi"]),
                new KeyValuePair<string, string>("response", token)
            });

            return await client.PostAsync(WebConfigurationManager.AppSettings["HCaptchaUrlApi"], content);
        }
    }
}
