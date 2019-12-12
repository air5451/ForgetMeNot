using Xamarin.Forms;
using ForgetMeNot.App.ViewModels;
using ForgetMeNot.App.Models;

namespace ForgetMeNot.App.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new CategoryViewModel(true);
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
    }
}

