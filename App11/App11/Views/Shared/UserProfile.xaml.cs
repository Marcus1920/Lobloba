using Acr.UserDialogs;
using App11.Services.SellersServices;
using App11.ViewModels.Logic;
using App11.Models.SellersModel;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProfile : ContentPage
	{
        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;

        private readonly AppFunctions _appFunctions = new AppFunctions();
        private MediaFile _mediafile;
        private const string NoSelection = "No selection made.";
        private string _latitude;
        private string _longitude;
        private bool _locationError;

        private readonly UserProfileService _service = new UserProfileService();
        private readonly Updateprofile _updateprofile = new Updateprofile();
        public UserProfile()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await GetUserProfile();

            base.OnAppearing();
        }

        public async Task GetUserProfile()
        {
            try
            {
                var profile = await _service.GetUser();

                BindingContext = profile;
            }
            catch (Exception)
            {
                await DisplayAlert("Error!",
                    "Connection interrupted. Please check your network status, refresh the page or try again later.",
                    "OK");

                await Navigation.PopAsync();
            }
        }

        private async void TakePicture_Clicked(object sender, EventArgs e)
        {

            _mediafile = await _appFunctions.TakePicture();

            if (_mediafile == null)
                await DisplayAlert(null, "Could not take picture.", "OK");

            else
                Image.Source = _mediafile.Path;
        }

        private async void BrowsePicture_Clicked__(object sender, EventArgs e)
        {
            _mediafile = await _appFunctions.BrowsePicture();

            if (_mediafile == null)
                await DisplayAlert(null, "Could not get picture.", "OK");

            else
                Image.Source = _mediafile.Path;
        }





        private async void Save_OnClicked(object sender, EventArgs e)
        {


            if (_mediafile == null)
            {
                UserDialogs.Instance.Alert("OK", "Please complete all the fields and set a product picture.", "Ok");
            }
            UserDialogs.Instance.ShowLoading("Loading....", MaskType.Gradient);

            _url = "http://system.foodforus.cloud/api/v1/updateProfile";

            var content = new MultipartFormDataContent();

            var values = new[]
            {
                new KeyValuePair<string, string>("api_key",_apiKey),
                new KeyValuePair<string, string>("email", email.Text),
                new KeyValuePair<string, string>("cellphone", phone.Text),

            };

            foreach (var keyValuePair in values)
            {
                content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
            }

            if (_mediafile != null)
                content.Add(new StreamContent(_mediafile.GetStream()), "\"profilePicture\"", "\"profilePicture\"");

            _response = await _client.PostAsync(_url, content);

            if (_response.IsSuccessStatusCode)

            {
                UserDialogs.Instance.HideLoading();
                try
                {
                    var profile = await _service.GetUser();

                    BindingContext = profile;
                    await Navigation.PopAsync();
                }
                catch (Exception)
                {
                    await DisplayAlert("Error!",
                        "Connection interrupted. Please check your network status, refresh the page or try again later.",
                        "OK");

                    await Navigation.PopAsync();
                }

            }
            else
            {
                UserDialogs.Instance.ShowError("erro ....");

            }





        }








    }
}