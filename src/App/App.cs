using Akavache;
using ForgetMeNot.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ForgetMeNot.App
{
    public class App : Application
    {
        public App()
        {
            BlobCache.ApplicationName = "ForgetMeNot";

            MainPage = new NavigationPage(new HomePage())
            {
                BarTextColor = Color.White,
                BarBackgroundColor = Color.FromHex("#F2C500")
            };
        }
    }
}

