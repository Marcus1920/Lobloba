using App11.Services.SellersServices;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
	public partial class ChangeLocation : ContentPage
	{
        private Position _currentLocation;
        private readonly LocationService _service = new LocationService();
        public ChangeLocation()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            GetcurentAsync();

            base.OnAppearing();
        }

        private async void GetcurentAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    _currentLocation = await CrossGeolocator.Current.GetPositionAsync(10000);

                    // lat.Text = "Lat: " + results.Latitude + " Long: " + results.Longitude;
                    TitleLabel.IsVisible = true;
                    IndicatorLabel.Text = "Lat: " + _currentLocation.Latitude + ", Long: " + _currentLocation.Longitude;
                    Indicator.IsVisible = false;
                    Save.IsVisible = true;
                    Cancel.IsVisible = true;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                // longit.Text = "Error: " + ex;
                await DisplayAlert("Location Error!",
                    "Could not access your current location. Please make sure your device location in turned on and try again.",
                    "OK");
                await Navigation.PopAsync();
            }
        }

        private void Cancel_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Save_OnClicked(object sender, EventArgs e)
        {
            Indicator.IsVisible = true;
            IndicatorLabel.Text = "Contacting server. Please wait...";
            TitleLabel.IsVisible = false;
            Save.IsVisible = false;
            Cancel.IsVisible = false;

            var response = await _service.SetPickUpPoint(_currentLocation);
            if (response)
            {
                await DisplayAlert(null, "Pick up point updated successfully.", "Ok");
            }

            else
            {
                await DisplayAlert("Error!", "Failed to communicate with the server. Please try again later.", "Ok");
            }

            await Navigation.PopAsync();
        }
    }
}