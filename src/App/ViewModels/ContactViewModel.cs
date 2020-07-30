using ForgetMeNot.App.Models;
using ForgetMeNot.App.Utils;
using System.Collections.ObjectModel;

namespace ForgetMeNot.App.ViewModels
{
    public class ContactViewModel
    {
        public Organization Organization { get; set; }

        public Friend.Contact Contact { get; set; }

        public ContactViewModel()
        {

        }
        public ContactViewModel(string organizationId, Friend.Contact contact)
        {
            Organization = OrganizationProvider.GetOrganization(organizationId, TokenProvider.GetToken());
            Contact = contact;
        }

        public bool IsBusy { get; set; }

    }
}
