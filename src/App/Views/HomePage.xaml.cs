namespace ForgetMeNot.App.Views
{
    using System;
    using ForgetMeNot.App.Models;
    using ForgetMeNot.App.LogOn;
    using Xamarin.Forms;
    using ForgetMeNot.App.ViewModels;
    using ForgetMeNot.App.Api;

    public partial class HomePage : ContentPage
    {
        private UserContext userContext = null;

        public HomePage()
        {
            InitializeComponent();
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e) => ((ListView)sender).SelectedItem = null;

        private async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var category = ((ListView)sender).SelectedItem as FriendCategory;
            if (category == null)
                return;

            await Navigation.PushAsync(new FriendsPage(category, this.userContext.PostalCode));
        }

        private async void OnSignInSignOut(object sender, EventArgs e)
        {
            await LogInLogOutActionAsync();
        }

        private async System.Threading.Tasks.Task LogInLogOutActionAsync()
        {
            try
            {
                if (btnSignInSignOut.Text == "Sign in")
                {
                    this.userContext = await B2CAuthenticationService.Instance.SignInAsync();
                    UpdateSignInState();
                }
                else
                {
                    this.userContext = await B2CAuthenticationService.Instance.SignOutAsync();
                    UpdateSignInState();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }

        private void UpdateSignInState()
        {
            var isSignedIn = userContext.IsLoggedOn;
            btnSignInSignOut.Text = isSignedIn ? "Sign out" : "Sign in";
            this.Refresh();
        }

        private void Refresh()
        {
            if (userContext.IsLoggedOn)
            {
                ApiCredentialProvider.SetApiCredentialsAsync(userContext);
                BindingContext = new CategoryViewModel(true);
            }
            else
            {
                BindingContext = new CategoryViewModel(false);
            } 
        }
    }
}

