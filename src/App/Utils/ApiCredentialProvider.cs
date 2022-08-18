using ForgetMeNot.App.LogOn;
using ForgetMeNot.App.Utils;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;

namespace ForgetMeNot.App.Api
{
    public class ApiCredentialProvider
    {
        public static async System.Threading.Tasks.Task SetApiCredentialsAsync(UserContext userContext)
        {
            var client = new RestClient("https://forgetmenotserver.azurewebsites.net/api/accountDetails");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {userContext.AccessToken}");
            var response = await client.ExecuteAsync(request);
            var cleaned = System.Text.RegularExpressions.Regex.Unescape(response.Content.Trim('"'));
            var credentials = JsonConvert.DeserializeObject<ApiCredentials>(cleaned);
            ConfigStore.ClientId = credentials.ClientId;
            ConfigStore.ClientSecret = credentials.ClientSecret;
        }
    }
}
