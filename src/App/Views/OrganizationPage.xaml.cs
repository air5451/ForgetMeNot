using System;
using System.Collections.Generic;
using ForgetMeNot.App.Models;
using ForgetMeNot.App.ViewModels;
using Xamarin.Essentials;
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Organization organization = ((OrganizationViewModel)BindingContext).Organization;
            Organization.Address address = organization.Addr;
            var placemark = new Placemark
            {
                CountryName = address.Country,
                AdminArea = address.State,
                Thoroughfare = address.Address1 + " " + address.Address2,
                Locality = address.City
            };
            var options = new MapLaunchOptions { Name = organization.Name };

            try
            {
                await Map.OpenAsync(placemark, options);
            }
            catch (Exception ex)
            {
                // No map application available to open or placemark can not be located
            }
        }
    }
}

