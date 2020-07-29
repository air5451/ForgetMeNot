using Newtonsoft.Json;

namespace ForgetMeNot.App.Api
{
    public class ApiCredentials
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }
        
        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }
    }
}
