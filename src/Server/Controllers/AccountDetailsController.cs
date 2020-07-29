using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/accountDetails")]
    [ApiController]
    public class AccountDetailsController : ControllerBase
    {
        // GET api/accountDetails
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var token = string.Empty;
            try
            {
                if (!this.User.Claims.Any(claim => claim.Type == "http://schemas.microsoft.com/identity/claims/scope" && claim.Value == "read"))
                {
                    return Unauthorized();
                }

                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                var tokenBundle = await keyVaultClient.GetSecretAsync("https://forgetmenot-keyvault.vault.azure.net/secrets/ApiConfigurations/a5a3737bd60a489cb7fafd884614bb56").ConfigureAwait(false);
                token = tokenBundle.Value;
            }
            catch (Exception exp)
            {
                return $"Something went wrong: {exp.Message}";
            }

            return token;
        }
    }
}
