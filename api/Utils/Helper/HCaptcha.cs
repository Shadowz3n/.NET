using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace API.Utils.Helper
{
    /// <summary>
    /// HCaptcha.
    /// </summary>
    public class HCaptcha
    {
        static readonly string _apiUrl = WebConfigurationManager.AppSettings["HCaptchaTokenApi"];
        static readonly string _apiToken = WebConfigurationManager.AppSettings["HCaptchaTokenApi"];

        /// <summary>
        /// Validate the specified token.
        /// </summary>
        /// <returns>The validate.</returns>
        /// <param name="token">Token.</param>
        public async Task<T> Validate<T>(string token)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _apiToken),
                new KeyValuePair<string, string>("response", token)
            });

            using (HttpClient client = new HttpClient())
            {
                var post = await client.PostAsync(_apiUrl, content);
                var response = await post.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(response);
            }
        }
    }
}
