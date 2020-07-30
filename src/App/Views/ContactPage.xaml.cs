using System;
using System.Collections.Generic;
using ForgetMeNot.App.Models;
using ForgetMeNot.App.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ForgetMeNot.App.Views
{
    public partial class ContactPage : ContentPage
    {
        public ContactPage(string organizationId, Friend.Contact contact)
        {
            InitializeComponent();

            ContactViewModel vvmContact = new ContactViewModel(organizationId, contact);
            BindingContext = vvmContact;

            Friend.Address addressData = contact.Address;

            this.AddressControl.Text = $"{addressData.Address1} {addressData.Address2} {addressData.City} {addressData.State} {addressData.Postcode}";
        }

        private async void Directions_Clicked(object sender, EventArgs e)
        {
            ContactViewModel vmContact = ((ContactViewModel)BindingContext);
            Organization organization = vmContact.Organization;
            Friend.Contact contact = vmContact.Contact;

            Friend.Address address = contact.Address;

            var placemark = new Placemark
            {
                CountryName = address.Country,
                AdminArea = address.State,
                Thoroughfare = address.Address1 + " " + address.Address2,
                Locality = address.City,
                PostalCode = address.Postcode
            };

            var options = new MapLaunchOptions { Name = organization.Name };
            await Map.OpenAsync(placemark, options);
        }

        private async void Email_Clicked(object sender, EventArgs e)
        {
            ContactViewModel vmContact = ((ContactViewModel)BindingContext);
            string email = vmContact.Contact.Email ?? vmContact.Organization.Email;

            var message = new EmailMessage
            {
                Subject = "",
                Body = "",
                To = new List<string> { email }
            };

            await Email.ComposeAsync(message);
        }

        private void Phone_Clicked(object sender, EventArgs e)
        {
            ContactViewModel vmContact = ((ContactViewModel)BindingContext);
            string phone = string.IsNullOrEmpty(vmContact.Contact.Phone) ? vmContact.Organization.Phone : vmContact.Contact.Phone;

            if (!string.IsNullOrEmpty(phone))
            {
                PhoneDialer.Open(phone);
            }
        }

        private async void Sms_Clicked(object sender, EventArgs e)
        {
            ContactViewModel vmContact = ((ContactViewModel)BindingContext);
            string phone = string.IsNullOrEmpty(vmContact.Contact.Phone) ? vmContact.Organization.Phone : vmContact.Contact.Phone;

            if (!string.IsNullOrEmpty(phone))
            {
                var message = new SmsMessage("", new[] { phone });
                await Sms.ComposeAsync(message);
            }
        }
    }
}

