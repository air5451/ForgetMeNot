using ForgetMeNot.App.Api;
using ForgetMeNot.App.Api.Types;
using ForgetMeNot.App.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.ObjectModel;

namespace ForgetMeNot.App.Utils
{
    public class OrganizationProvider
    {
        public static Models.Organization GetOrganization(string organizationid, string token)
        {
            var baseUrl = ConfigStore.PetFinderApiBaseUrl;
            var relativeUrl = $"/v2/organizations/" + organizationid;

            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api.petfinder.com");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
            request.AddHeader("Authorization", $"Bearer {token}");

            var client = new RestClient($"{baseUrl}{relativeUrl}");
            var response = client.Execute(request);

            Api.Organization organization = JsonConvert.DeserializeObject<OrganizationResponse>(response.Content).organization;

            return new Models.Organization
            {
                Id = organization.id,
                Name = organization.name,
                Addr = new Models.Organization.Address
                {
                    Address1 = organization.address.address1,
                    Address2 = organization.address.address2,
                    City = organization.address.city,
                    State = organization.address.state,
                    Country = organization.address.country,
                    Postcode = organization.address.postcode
                },
                Distance = organization.distance,
                Email = organization.email,
                Phone = organization.phone,
                Hrs = new Models.Organization.Hours
                {
                    Monday = organization.hours.monday,
                    Tuesday = organization.hours.tuesday,
                    Wednesday = organization.hours.wednesday,
                    Thursday = organization.hours.thursday,
                    Friday = organization.hours.friday,
                    Saturday = organization.hours.saturday,
                    Sunday = organization.hours.sunday
                },
                Website = organization.website
            };
        }
    }
}
