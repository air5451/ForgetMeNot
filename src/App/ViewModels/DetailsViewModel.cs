using ForgetMeNot.App.Models;

namespace ForgetMeNot.App.ViewModels
{
    public class DetailsViewModel
    {
        public Friend Friend { get; set; }
        public DetailsViewModel(Friend friend)
        {
            Friend = friend;
        }
    }
}

