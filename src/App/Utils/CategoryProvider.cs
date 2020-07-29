using Akavache;
using ForgetMeNot.App.Api.Types;
using ForgetMeNot.App.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ForgetMeNot.App.Utils
{
    public class CategoryProvider
    {
        private static readonly string cacheKey = "FriendCategories";

        public static ObservableCollection<FriendCategory> GetFriendCategories(string token)
        {
            try
            {
                Task<FriendCategory[]> getCategoryTask = Task.Run(() => CheckCacheAsync(cacheKey));
                getCategoryTask.Wait();
                FriendCategory[] result = getCategoryTask.Result;

                if (result != null)
                {
                    return new ObservableCollection<FriendCategory>(result);
                }
            }
            catch (Exception) { }

            var baseUrl = ConfigStore.PetFinderApiBaseUrl;
            var relativeUrl = $"/v2/types";

            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "api.petfinder.com");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
            request.AddHeader("Authorization", $"Bearer {token}");

            var collection = new ObservableCollection<FriendCategory>();

            var client = new RestClient($"{baseUrl}{relativeUrl}");
            var response = client.Execute(request);

            var categories = JsonConvert.DeserializeObject<FriendCategoryResponse>(response.Content);

            foreach (var category in categories.types)
            {
                var parts = category._links.self.href.Split('/');
                var type = parts[parts.Length - 1];

                //GetFriends returns collection of groups, where each group collection of Friends
                var group = FriendProvider.GetFriends(token, type, Global.ZipCode, Global.Distance).FirstOrDefault();

                if (group == null) continue;

                string image = null;

                foreach (var friend in group)
                {
                    if (friend != null) 
                    {
                        image = friend.Image;
                        break;
                    }
                }

                collection.Add(new FriendCategory
                {
                    Name = category.name,
                    Type = type,
                    Image = image ?? ConfigStore.DefaultFriendPicture
                });
            }

            BlobCache.LocalMachine.InsertObject<FriendCategory[]>(cacheKey, collection.ToArray());

            return collection;
        }

        private static async Task<FriendCategory[]> CheckCacheAsync(string cacheKey)
        {
            return await BlobCache.LocalMachine.GetObject<FriendCategory[]>(cacheKey);
        }
    }
}
