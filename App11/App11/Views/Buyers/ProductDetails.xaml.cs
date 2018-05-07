using Acr.UserDialogs;
using App11.Models.BuyersModel;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Buyers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductDetails : ContentPage
	{
        Product product = new Product();
        public ProductDetails(Product product)
        {
            BindingContext = product;
            this.product = product;
            InitializeComponent();
        }

        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserProductCart());
        }

        //BindingContext = new ContentPageViewModel();
        async void OnAddToCartClicked(object sender, EventArgs args)
        {
            // var postData = new List<KeyValuePair<string, string>>();
            //postData.Add(new KeyValuePair<string, string>("foodItem", "Milk"));
            //postData.Add(new KeyValuePair<string, string>("quantity", "20"));

           // string userApiKey = CrossSettings.Current.GetValueOrDefault("APIKey", "unknown");
            string ApiKey = (Application.Current as App).UserTokenKey;

            try
            {
                var values = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("api_key",ApiKey),
                    new KeyValuePair<string,string>("foodItem",this.product.productTypeId),
                    new KeyValuePair<string,string>("quantity",quantity.Text),
                    new KeyValuePair<string, string>("id",this.product.id)
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
                await DisplayAlert("", ApiKey, "OK");
                await DisplayAlert("", ex.StackTrace, "OK");
            }
        }

        private async void mapview_Clicked(object sender, EventArgs e)
        {
            var latt = gps_lat.Text;


            var longit = gps_long.Text;

            if (String.IsNullOrWhiteSpace(gps_lat.Text) || String.IsNullOrWhiteSpace(gps_long.Text))
            {

                await DisplayAlert("", "This  post doesn't  have  gps Location ", "Ok");
            }
            else
            {
                await Navigation.PushAsync(new PinckupMap());
            }

        }

        private void ToolbarItem_OnActivatedOnActivatedChat(object sender, EventArgs e)
        {


            Navigation.PushAsync(new BuyersChat(product));
        }
    }

    class ProductDetailsViewModel : INotifyPropertyChanged
    {

        public ProductDetailsViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);
        }

        int count;

        string countDisplay = "You clicked 0 times.";
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}