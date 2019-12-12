using ForgetMeNot.App.Utils;

namespace ForgetMeNot.App.Models
{
    public class FriendCategory
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Image { get; set; } = ConfigStore.DefaultFriendPicture;

    }
}
