using Newtonsoft.Json;

namespace API.Models
{
    public class GoogleResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
