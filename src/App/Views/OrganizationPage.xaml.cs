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

            OrganizationViewModel vvmOrg = new OrganizationViewModel(organizationId);
            BindingContext = vvmOrg;

            Organization.Address addressData = vvmOrg.Organization.Addr;
            Organization.Hours hoursData = vvmOrg.Organization.Hrs;

            this.AddressControl.Text = $"{addressData.Address1} {addressData.Address2} {addressData.City} {addressData.State}";
            this.HoursControl.Text = $"M {hoursData.Monday}, T {hoursData.Tuesday}, W {hoursData.Wednesday}, T {hoursData.Thursday}, F {hoursData.Friday}, S {hoursData.Saturday}, S {hoursData.Sunday}";
        }

        private async void Directions_Clicked(object sender, EventArgs e)
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
            await Map.OpenAsync(placemark, options);
        }

        private async void Email_Clicked(object sender, EventArgs e)
        {
            var message = new EmailMessage
            {
                Subject = "",
                Body = "",
                To = new List<string> { ((OrganizationViewModel)BindingContext).Organization.Email }
            };

            await Email.ComposeAsync(message);
        }

        private void Phone_Clicked(object sender, EventArgs e)
        {
            PhoneDialer.Open(((OrganizationViewModel)BindingContext).Organization.Phone);
        }

        private async void Sms_Clicked(object sender, EventArgs e)
        {
            var message = new SmsMessage("", new[] { ((OrganizationViewModel)BindingContext).Organization.Phone });
            await Sms.ComposeAsync(message);
        }
    }
}

