namespace ForgetMeNot.App.Api
{
    using Newtonsoft.Json;

    public class ApiCredentials
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }
        
        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }
    }
}
