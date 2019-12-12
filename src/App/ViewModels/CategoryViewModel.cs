using ForgetMeNot.App.Models;
using ForgetMeNot.App.Utils;
using System.Collections.ObjectModel;

namespace ForgetMeNot.App.ViewModels
{
    public class CategoryViewModel
    {
        public ObservableCollection<FriendCategory> FriendCategories { get; set; }

        public CategoryViewModel()
        {

        }
        public CategoryViewModel(bool designData)
        {
            if (designData)
            {
                FriendCategories = CategoryProvider.GetFriendCategories(TokenProvider.GetToken());
            }
            else
            {
                //Setup web requests and such
            }
        }

        public bool IsBusy { get; set; }

        public int CategoriesCount => FriendCategories.Count;

    }
}
