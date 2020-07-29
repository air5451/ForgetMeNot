using ForgetMeNot.App.Models;
using ForgetMeNot.App.Utils;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ForgetMeNot.App.ViewModels
{
    public class CategoryViewModel
    {
        public ObservableCollection<FriendCategory> FriendCategories { get; set; }

        public CategoryViewModel()
        {

        }

        private bool DesignData { get; set; }  = false;

        public CategoryViewModel(bool designData)
        {
            DesignData = designData;
        }

        public async Task<CategoryViewModel> Init()
        {
            if (DesignData)
            {
                FriendCategories = await CategoryProvider.GetFriendCategories(TokenProvider.GetToken());
            }
            else
            {
                //Setup web requests and such
            }
            return this;
        }

        public bool IsBusy { get; set; }

        public int CategoriesCount => FriendCategories.Count;

    }
}
