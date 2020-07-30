using System;
using System.Collections.Generic;
using ForgetMeNot.App.Models;
using ForgetMeNot.App.ViewModels;
using Xamarin.Forms;

namespace ForgetMeNot.App.Views
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Friend friend)
        {
            InitializeComponent();
            BindingContext = new DetailsViewModel(friend);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Friend friend = ((DetailsViewModel)BindingContext).Friend;
            await Navigation.PushAsync(new ContactPage(friend.Organization, friend.Contacts));
        }
    }
}

