using System.Text.Json.Serialization;

namespace Web.Shared.Dto
{
    public class LoginResult
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
