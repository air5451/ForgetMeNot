using ForgetMeNot.App.Models;
using ForgetMeNot.App.Utils;
using System.Collections.ObjectModel;

namespace ForgetMeNot.App.ViewModels
{
    public class OrganizationViewModel
    {
        public Organization Organization { get; set; }

        public OrganizationViewModel()
        {

        }
        public OrganizationViewModel(string organizationId)
        {
            Organization = OrganizationProvider.GetOrganization(organizationId, TokenProvider.GetToken());
        }

        public bool IsBusy { get; set; }

    }
}
