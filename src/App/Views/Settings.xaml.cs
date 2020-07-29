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
        private bool zipcoderegex = false;
        private bool distanceregex = false;

        public int zipcode = Global.ZipCode;
        public int Zipcode
        {
            get { return zipcode; }
            set { zipcode = value; }
        }

        public int distance = Global.Distance;
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = this;
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
            zipcoderegex = result;

            if (zipcoderegex && distanceregex)
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
            distanceregex = result;

            if (zipcoderegex && distanceregex)
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