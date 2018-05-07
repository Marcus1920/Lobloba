using App11.Views.Buyers;
using App11.Views.Researchers;
using App11.Views.Sellers;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SwitshPage : ContentPage
	{
        public SwitshPage()
        {
            InitializeComponent();

            UserIcon.Source = ImageSource.FromFile("logo.png");
        }

        //  public string Role => roles.SelectedItem.ToString();
        /// public Button SwitchAcc => swicth;

        private async void swicth_Clicked(object sender, EventArgs e)
        {

            if (roles.SelectedItem == "Buyer")
            {

                CrossSettings.Current.AddOrUpdateValue("userrole", "Buyer");
                //      await Navigation.PushAsync(new BuyersHome());
                await Navigation.PushModalAsync(new BuyersHome());


            }

            else if (roles.SelectedItem == "Seller")
            {
                CrossSettings.Current.AddOrUpdateValue("userrole", "Seller");
                //   await Navigation.PushAsync(new SellersMaster());
               await Navigation.PushModalAsync(new SellersMaster());
            }
            else if (roles.SelectedItem == "Researcher")
            {
                CrossSettings.Current.AddOrUpdateValue("userrole", "Researcher");
                  await Navigation.PushAsync(new ResearcherMaster());
             //   await Navigation.PushModalAsync(new ResearcherMaster());


            }

        }
    }
}