namespace ForgetMeNot.App.Droid
{
    using ForgetMeNot.App.LogOn;
    using Plugin.CurrentActivity;

    class AndroidParentWindowLocatorService : IParentWindowLocatorService
    {
        public object GetCurrentParentWindow()
        {
            return CrossCurrentActivity.Current.Activity;
        }
    }
}