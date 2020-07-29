using ForgetMeNot.App.Models;
using ForgetMeNot.App.Utils;
using System.Collections.ObjectModel;

namespace ForgetMeNot.App.ViewModels
{
    public class FriendsViewModel
    {
        public ObservableCollection<Grouping<string, Friend>> FriendsGrouped { get; set; }

        public FriendsViewModel()
        {

        }
        public FriendsViewModel(FriendCategory category, int zip)
        {
            FriendsGrouped = FriendProvider.GetFriends(TokenProvider.GetToken(), category.Type, zip, 5);
        }

        public bool IsBusy { get; set; }

        public int FriendsCount
        {
            get
            {
                var friendsCount = 0;
                foreach (var group in FriendsGrouped)
                {
                    friendsCount += group.Count;
                }
                return friendsCount;
            }
        }
    }
}
