using System;
using System.Collections.Generic;
using ForgetMeNot.App.Models;
using ForgetMeNot.App.ViewModels;
using Xamarin.Forms;

namespace ForgetMeNot.App.Views
{
    public partial class OrganizationPage : ContentPage
    {
        public OrganizationPage(string organizationId)
        {
            InitializeComponent();
            BindingContext = new OrganizationViewModel(organizationId);
        }
    }
}

