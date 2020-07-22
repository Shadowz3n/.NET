using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using API.Services;
using API.Models;

namespace API.Utils.Helper
{

    public class HCaptcha : BaseService
    {
        private readonly string _secretKey;

        public HCaptcha(string url, string secretKey) : base(url)
        {
            _secretKey = secretKey;
        }

        public async Task<HCaptchaResponse> ValidateFromGoogle(string token)
        {

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _secretKey),
                new KeyValuePair<string, string>("response", token)
            });

            return await PostAsync<HCaptchaResponse>("", content);
        }
    }
}
