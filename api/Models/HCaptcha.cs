using Newtonsoft.Json;

namespace API.Models
{
    /// <summary>
    /// HC aptcha response.
    /// </summary>
    public class HCaptchaResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:API.Models.HCaptchaResponse"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
