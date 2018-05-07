using Acr.UserDialogs;
using App11.Models;
using App11.Models.SellersModel;
using App11.Services.SellersServices;
using App11.Services.ShareService;
using App11.ViewModels;
using App11.ViewModels.Logic;
using App11.Views.Sellers;
using Newtonsoft.Json;
using Plugin.LocalNotifications;
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
	public partial class IntrestbuyerProduct : ContentPage
	{
        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;

        private readonly SellersPostsService _service = new SellersPostsService();
        private readonly AppFunctions _appFunctions = new AppFunctions();

        private readonly UpdateIntrestServices _updateIntrest = new UpdateIntrestServices();

        private const string NoSelection = "No selection made.";

        public IntrestbuyerProduct()
        {
            InitializeComponent();
            ProductTypes.Text = NoSelection;
            ProductTypetwo.Text = NoSelection;
            ProductTypesthree.Text = NoSelection;
            BindingContext = new UpdateIntresatViewModel();
        }

        private async void Save_OnClickedicked(object sender, EventArgs e)
        {





            _url = " http://dev.foodforus.cloud/public/api/v1/productInterest";


            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("api_key", _apiKey),
                new KeyValuePair<string, string>("productInterest1",ProductTypes.Text),
                new KeyValuePair<string, string>("productInterest2",ProductTypetwo.Text),
                new KeyValuePair<string, string>("productInterest3",ProductTypesthree.Text),


            });

            _response = await _client.PostAsync(_url, form);



            var respond = await _response.Content.ReadAsStringAsync();

            loginModel responses = JsonConvert.DeserializeObject<loginModel>(respond);

            UserDialogs.Instance.Alert("Notification", "Successfully updated product  Alerts", "OK");

            CrossLocalNotifications.Current.Show("FoodForus ", "Successfully updated product  Alerts", 101, DateTime.Now.AddSeconds(5));
            await Navigation.PushModalAsync(new BuyersHome());
        }

        private async Task Button_OnClickedductType_OnClicked(object sender, EventArgs e)
        {
            var page = new ProductTypes();
            page.ProductTypesListView.ItemSelected += async (source, args) =>
            {
                var type = args.SelectedItem as ProductType;
                if (type != null) ProductTypes.Text = type.Name.ToString();
                await Navigation.PopAsync();
            };

            await Navigation.PushAsync(page);
        }


        private async Task Button_OnClickedductType_OnClicked_ThirdSection(object sender, EventArgs e)
        {

            var page = new ProductTypes();
            page.ProductTypesListView.ItemSelected += async (source, args) =>
            {
                var type = args.SelectedItem as ProductType;
                if (type != null) ProductTypesthree.Text = type.Name.ToString();
                await Navigation.PopAsync();
            };

            await Navigation.PushAsync(page);
        }

        private async Task Button_OnClickedductType_OnClickedSecondSelection(object sender, EventArgs e)
        {
            var page = new ProductTypes();
            page.ProductTypesListView.ItemSelected += async (source, args) =>
            {
                var type = args.SelectedItem as ProductType;
                if (type != null) ProductTypetwo.Text = type.Name.ToString();
                await Navigation.PopAsync();
            };

            await Navigation.PushAsync(page);
        }
    }
}