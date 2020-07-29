using System;
using System.Collections.Generic;
using System.Text;

namespace ForgetMeNot.App.Utils
{
    public static class ConfigStore
    {
        public static string PetFinderApiBaseUrl => "https://api.petfinder.com";

        public static string DefaultFriendPicture => "https://s3.amazonaws.com/petfinder-us-east-1-petimages-prod/organization-photos/39536/39536-1.jpg?bust=2017-12-04+18%3A02%3A01&width=100";

        public static object ClientId { get; set; } = "";

        public static object ClientSecret { get; set; } = "";
    }
}
