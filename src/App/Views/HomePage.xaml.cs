using System;
using System.Threading.Tasks;
using ForgetMeNot.App.Models;
using ForgetMeNot.App.LogOn;
using ForgetMeNot.App.ViewModels;
using ForgetMeNot.App.Api;
using ForgetMeNot.App.Utils;
using Xamarin.Forms;

namespace ForgetMeNot.App.Views
{
    public partial class HomePage : ContentPage
    {
        private UserContext userContext = null;

        public HomePage()
        {
            InitializeComponent();
        }

        public async Task<HomePage> Init()
        {
            await this.LogInLogOutActionAsync();
            return this;
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e) => ((ListView)sender).SelectedItem = null;

        private async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var category = ((ListView)sender).SelectedItem as FriendCategory;
            if (category == null)
                return;
            await Navigation.PushAsync(new FriendsPage(category));
        }
        
        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void OnSignInSignOut(object sender, EventArgs e)
        {
            await LogInLogOutActionAsync();
        }

        private async Task LogInLogOutActionAsync()
        {
            try
            {
                if (this.userContext == null || !this.userContext.IsLoggedOn)
                {
                    this.userContext = await B2CAuthenticationService.Instance.SignInAsync();
                    await UpdateSignInStateAsync();
                }
                else
                {
                    this.userContext = await B2CAuthenticationService.Instance.SignOutAsync();
                    await UpdateSignInStateAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert($"Exception:", ex.ToString(), "Dismiss");
            }
        }

        private async Task UpdateSignInStateAsync()
        {
            var isSignedIn = this.userContext.IsLoggedOn;

            if (!isSignedIn)
            {
                BindingContext = await new CategoryViewModel(false).Init();
            }
            else
            {
                int zipCodeFromToken;
                if (int.TryParse(this.userContext.PostalCode, out zipCodeFromToken))
                {
                    Global.ZipCode = zipCodeFromToken;
                }
                await ApiCredentialProvider.SetApiCredentialsAsync(this.userContext);
                BindingContext = await new CategoryViewModel(true).Init();
            }

            btnSignInSignOut.Text = isSignedIn ? "Sign out" : "Sign in";
        }
    }
}

