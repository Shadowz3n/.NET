using Newtonsoft.Json;

namespace API.Models
{
    public class HCaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
