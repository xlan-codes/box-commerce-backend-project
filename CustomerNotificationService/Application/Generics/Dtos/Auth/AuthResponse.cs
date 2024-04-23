using Newtonsoft.Json;

namespace Application.Generics.Dtos.Auth.Cisl
{
    public class AuthResponse
    {
        [JsonProperty("classId")]
        public string ClassId { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiryTime { get; set; }
    }
}
