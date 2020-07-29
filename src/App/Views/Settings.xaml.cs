using Akavache;
using ForgetMeNot.App.Utils;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForgetMeNot.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private int zipcode = 0;
        private int distance = 0;

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //check if the zipcode and distance is new
            bool invalidate = Global.ZipCode != this.zipcode && Global.Distance != this.distance;

            Global.ZipCode = this.zipcode;
            Global.Distance = this.distance;

            if (invalidate)
            {
                BlobCache.LocalMachine.InvalidateAll();
            }

            Navigation.PopAsync();
        }

        private void Entry_TextChangedZipcode(object sender, TextChangedEventArgs e)
        {
            string text = (sender as Entry).Text;
            int i = 0;
            var button = this.FindByName<Button>("button");

            bool result = int.TryParse(text, out i);

            if (result)
            {
                button.IsEnabled = true;
                this.zipcode = i;
            }
            else
            {
                button.IsEnabled = false;
            }
        }

        private void Entry_TextChangedDistance(object sender, TextChangedEventArgs e)
        {
            string text = (sender as Entry).Text;
            int i = 0;
            var button = this.FindByName<Button>("button");

            bool result = int.TryParse(text, out i);

            if (result)
            {
                button.IsEnabled = true;
                this.distance = i;
            }
            else
            {
                button.IsEnabled = false;
            }
        }
    }
}