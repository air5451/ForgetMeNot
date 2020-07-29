using Akavache;
using ForgetMeNot.App.Views;
using System.Threading.Tasks;
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
        }

        public async Task<App> Init()
        {
            MainPage = new NavigationPage(await new HomePage().Init())
            {
                BarTextColor = Color.White,
                BarBackgroundColor = Color.FromHex("#F2C500")
            };
            return this;
        }
    }
}

