using ForgetMeNot.App.ViewModels;
using ForgetMeNot.App.Models;
using Xamarin.Forms;

namespace ForgetMeNot.App.Views
{
    public partial class FriendsPage : ContentPage
    {
        public FriendsPage(FriendCategory category)
        {
            InitializeComponent();
            BindingContext = new FriendsViewModel(category);
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e) => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var friend = ((ListView)sender).SelectedItem as Friend;
            if (friend == null)
                return;

            await Navigation.PushAsync(new DetailsPage(friend));
        }
    }
}

