using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Sellers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellersTabbedPage : TabbedPage
    {
        public SellersTabbedPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Exit page?", "Are you sure you want to exit this page? You will will be logged out if you continue.", "Yes, continue", "No"))
                {
                    CrossSettings.Current.AddOrUpdateValue("userlogin", "false");
                    base.OnBackButtonPressed();

                    await Navigation.PopModalAsync();
                }
            });

            return true;
        }
    }
}