namespace ForgetMeNot.App.Api
{
    using ForgetMeNot.App.LogOn;
    using ForgetMeNot.App.Utils;
    using Newtonsoft.Json;
    using RestSharp;
    using RestSharp.Extensions;

    public class ApiCredentialProvider
    {
        public static void SetApiCredentialsAsync(UserContext userContext)
        {
            var client = new RestClient("https://forgetmenotserver.azurewebsites.net/api/accountDetails");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {userContext.AccessToken}");
            var response = client.Execute(request);
            var cleaned = System.Text.RegularExpressions.Regex.Unescape(response.Content.RemoveSurroundingQuotes());
            var credentials = JsonConvert.DeserializeObject<ApiCredentials>(cleaned);
            ConfigStore.ClientId = credentials.ClientId;
            ConfigStore.ClientSecret = credentials.ClientSecret;
        }
    }
}
