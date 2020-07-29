using System;
using System.Collections.Generic;
using ForgetMeNot.App.ViewModels;
using Xamarin.Forms;
using ForgetMeNot.App.Models;
using System.ComponentModel;

namespace ForgetMeNot.App.Views
{
    public partial class FriendsPage : ContentPage
    {
        public FriendsPage(FriendCategory category, string zip)
        {
            InitializeComponent();

            int zipInt = 98004;
            int.TryParse(zip, out zipInt);

            BindingContext = new FriendsViewModel(category, zipInt);
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var friend = ((ListView)sender).SelectedItem as Friend;
            if (friend == null)
                return;

            await Navigation.PushAsync(new DetailsPage(friend));
        }
    }
}

