using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using API.Services;
using API.Models;

namespace API.Utils.Helper
{

    public class GoogleRecaptcha : BaseService
    {
        private readonly string _secretKey;

        public GoogleRecaptcha(string url, string secretKey) : base(url)
        {
            _secretKey = secretKey;
        }

        public async Task<GoogleResponse> ValidateFromGoogle(string token)
        {

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _secretKey),
                new KeyValuePair<string, string>("response", token)
            });

            return await PostAsync<GoogleResponse>("", content);
        }
    }
}
