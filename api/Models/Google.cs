using Newtonsoft.Json;

namespace API.Models
{
    /// <summary>
    /// Google response.
    /// </summary>
    public class GoogleResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:API.Models.GoogleResponse"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
