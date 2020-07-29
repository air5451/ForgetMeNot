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
using Xamarin.Essentials;

namespace ForgetMeNot.App.Utils
{
    public class CategoryProvider
    {
        private static readonly string cacheKey = "FriendCategories";

        public static async Task<ObservableCollection<FriendCategory>> GetFriendCategories(string token)
        {
            var result = await CheckCacheAsync(cacheKey);

            if (result != null)
            {
                return result;
            }

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
            var response = await client.ExecuteTaskAsync(request);

            var categories = JsonConvert.DeserializeObject<FriendCategoryResponse>(response.Content);

            foreach (var category in categories.types)
            {
                var parts = category._links.self.href.Split('/');
                var type = parts[parts.Length - 1];

                //GetFriends returns collection of groups, where each group collection of Friends
                var group = FriendProvider.GetFriends(token, type, await LocationProvider.GetZip(), 5).FirstOrDefault();

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

            return await UpdateCacheAsync(cacheKey, collection);
        }

        private static async Task<ObservableCollection<FriendCategory>> CheckCacheAsync(string cacheKey)
        {
            try
            {
                var cachedCategories = await BlobCache.LocalMachine.GetObject<FriendCategory[]>(cacheKey);

                return cachedCategories == null
                    ? null
                    : new ObservableCollection<FriendCategory>(cachedCategories);
            }
            catch
            {
                // TODO: logging to app insights? 
                return null;
            }
        }

        private static async Task<ObservableCollection<FriendCategory>> UpdateCacheAsync(string cacheKey, ObservableCollection<FriendCategory> categories)
        {
            await BlobCache.LocalMachine.InsertObject<FriendCategory[]>(cacheKey, categories.ToArray());
            return categories;
        }
    }
}
