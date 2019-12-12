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
    }
}

