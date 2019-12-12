using ForgetMeNot.App.Api;
using Newtonsoft.Json;
using RestSharp;

namespace ForgetMeNot.App.Utils
{
    public static class TokenProvider
    {
        public static string GetToken()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials&client_id={ConfigStore.ClientId}&client_secret={ConfigStore.ClientSecret}", ParameterType.RequestBody);

            var baseUrl = ConfigStore.PetFinderApiBaseUrl;
            var relativeUrl = "/v2/oauth2/token";

            var client = new RestClient($"{baseUrl}{relativeUrl}");
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<TokenValidationResponse>(response.Content).Access_token;
        }
    }
}
