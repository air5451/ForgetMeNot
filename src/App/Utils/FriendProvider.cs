using Akavache;
using DynamicData.Binding;
using ForgetMeNot.App.Api;
using ForgetMeNot.App.Models;
using Newtonsoft.Json;
using RestSharp;
using SQLitePCL;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ForgetMeNot.App.Utils
{
    public static class FriendProvider
    {
        public static ObservableCollection<Grouping<string, Friend>> GetFriends(string token, string type, int zip, int distance)
        {
            string cacheKey = string.Join("-", type, zip, distance);

            try
            {
                Task<Friend[]> getStringTask = Task.Run(() => CheckCacheAsync(cacheKey));
                getStringTask.Wait();
                Friend[] result = getStringTask.Result;

                if (result != null)
                {
                    return SortCollection(new ObservableCollection<Friend>(result));
                }
            }
            catch (Exception) { }

            var baseUrl = ConfigStore.PetFinderApiBaseUrl;
            var relativeUrl = $"/v2/animals?type={type}&location={zip}&distance={distance}&status=adoptable";

            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api.petfinder.com");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
            request.AddHeader("Authorization", $"Bearer {token}");

            var collection = new ObservableCollection<Friend>();

            do
            {
                var client = new RestClient($"{baseUrl}{relativeUrl}");
                var response = client.Execute(request);

                var friends = JsonConvert.DeserializeObject<FriendResponse>(response.Content);

                foreach (var friend in friends.animals)
                {
                    var defaultUrl = ConfigStore.DefaultFriendPicture;
                    collection.Add(new Friend
                    {
                        Name = friend.name,
                        Location = $"{friend.contact.address.city}, {friend.contact.address.state} {friend.contact.address.postcode}",
                        Details = HttpUtility.UrlDecode(HttpUtility.UrlDecode(friend.description)),
                        Image = friend.photos.Count == 0 ? defaultUrl : friend.photos.FirstOrDefault().Small,
                        Breed = friend.breeds.primary,
                        Published = friend.published_at.ToLocalTime().ToString(),
                        Organization = friend.organization_id
                    });
                }

                if (friends.pagination != null && friends.pagination._links != null && friends.pagination._links.next != null)
                {
                    relativeUrl = friends.pagination._links.next.href;
                }
                else
                {
                    relativeUrl = null;
                }
            }
            while (relativeUrl != null);

            var friendCollection = SortCollection(collection);

            BlobCache.LocalMachine.InsertObject<Friend[]>(cacheKey, collection.ToArray());

            return friendCollection;
        }

        private static ObservableCollection<Grouping<string, Friend>> SortCollection(ObservableCollection<Friend> collection)
        {
            var sorted = from friend in collection
                         orderby friend.Breed
                         group friend by friend.SortProperty into friendGroup
                         select new Grouping<string, Friend>(friendGroup.Key, friendGroup);

            return new ObservableCollection<Grouping<string, Friend>>(sorted);
        }

        private static async Task<Friend[]> CheckCacheAsync(string cacheKey)
        {
            return await BlobCache.LocalMachine.GetObject<Friend[]>(cacheKey);
        }
    }
}
