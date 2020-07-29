namespace ForgetMeNot.App.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Xamarin.Forms;
    using Plugin.CurrentActivity;
    using ForgetMeNot.App.LogOn;
    using Microsoft.Identity.Client;

    [Activity(Label = "ForgetMeNot.App.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            CrossCurrentActivity.Current.Init(this, bundle);
            DependencyService.Register<IParentWindowLocatorService, AndroidParentWindowLocatorService>();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}