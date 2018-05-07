using Acr.UserDialogs;
using App11.Models;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Buyers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationDetails : ContentPage
	{
        NotificationMessageModel notificationMessa = new NotificationMessageModel();

        public NotificationDetails(NotificationMessageModel notificationMessa)
        {
            BindingContext = notificationMessa;
            this.notificationMessa = notificationMessa;
            InitializeComponent();
        }

        private void Mapview_OnClickedClicked(object sender, EventArgs e)
        {

        }

        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserProductCart());
        }

        private void ToolbarItem_OnActivatedChat(object sender, EventArgs e)
        {
            /// Navigation.PushAsync(new Chat());
        }

        async void OnAddToCartClicked(object sender, EventArgs e)
        {

            string userApiKey = CrossSettings.Current.GetValueOrDefault("APIKey", "unknown");

            try
            {
                var values = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("api_key",userApiKey),
                    new KeyValuePair<string,string>("foodItem",this.notificationMessa.productTypeId),
                    new KeyValuePair<string,string>("quantity",quantity.Text),
                    new KeyValuePair<string, string>("id",this.notificationMessa.PostId)
                });

                var client = new HttpClient();
                var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/addToCart", values);
                var respond = await response.Content.ReadAsStringAsync();

                //await DisplayAlert("", "OK", "OK");
                //  await DisplayAlert("", "Successfully Added To Cart", "OK");
                UserDialogs.Instance.ShowSuccess("Successfully Added To Cart", 2000);

                await Navigation.PushModalAsync(new BuyersHome());

            }
            catch (Exception ex)
            {
                await DisplayAlert("", userApiKey, "OK");
                await DisplayAlert("", ex.StackTrace, "OK");
            }

        }

        private async void Remove_OnClickedAsync(object sender, EventArgs e)
        {

            string userApiKey = CrossSettings.Current.GetValueOrDefault("APIKey", "unknown");

            try
            {
                var values = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("id",this.notificationMessa.id)
                });

                var client = new HttpClient();
                var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/removeNotification", values);
                var respond = await response.Content.ReadAsStringAsync();

                //await DisplayAlert("", "OK", "OK");
                //  await DisplayAlert("", "Successfully Added To Cart", "OK");
                UserDialogs.Instance.ShowSuccess("Notification Remove", 2000);

                await Navigation.PushModalAsync(new BuyersHome());

            }
            catch (Exception ex)
            {
                await DisplayAlert("", userApiKey, "OK");
                await DisplayAlert("", ex.StackTrace, "OK");
            }



        }
    }
}