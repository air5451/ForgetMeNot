using Xamarin.Forms;
using ForgetMeNot.App.ViewModels;
using ForgetMeNot.App.Models;
using System.Threading.Tasks;

namespace ForgetMeNot.App.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public async Task<HomePage> Init()
        {
            BindingContext = await new CategoryViewModel(true).Init();
            return this;
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var category = ((ListView)sender).SelectedItem as FriendCategory;
            if (category == null)
                return;

            await Navigation.PushAsync(new FriendsPage(category));
        }

        async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}

