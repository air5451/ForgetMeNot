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
    public class LocationProvider
    {
        public static async Task<Location> GetLocation()
        {
            return await Geolocation.GetLastKnownLocationAsync();
        }

        public static async Task<int> GetZip()
        {
            var location = await GetLocation();
            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var placemark = placemarks.FirstOrDefault();
            return Convert.ToInt32(placemark.PostalCode);
        }
    }
}
