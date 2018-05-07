using App11.Models.SellersModel;
using App11.Services.SellersServices;
using App11.ViewModels.Logic;
using App11.Views.Sellers;
using Plugin.Geolocator;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
	public partial class AddNewDonation : ContentPage
	{
        private readonly SellersPostsService _service = new SellersPostsService();
        private readonly AppFunctions _appFunctions = new AppFunctions();
        private MediaFile _mediafile;
        private const string NoSelection = "No Type selected.";
        private string _latitude;
        private string _longitude;

        public AddNewDonation()
        {
            InitializeComponent();

            ProductType.Text = NoSelection;
            PackagingType.Text = NoSelection;
            SellBy.MinimumDate = DateTime.Now;
        }

        protected override void OnAppearing()
        {
            GetcurentAsync();

            base.OnAppearing();
        }

        private async Task GetProductType_OnClicked(object sender, EventArgs e)
        {
            var page = new ProductTypes();
            page.ProductTypesListView.ItemSelected += async (source, args) =>
            {
                var type = args.SelectedItem as ProductType;
                if (type != null) ProductType.Text = type.Name.ToString();
                await Navigation.PopAsync();
            };

            await Navigation.PushAsync(page);
        }

        private async Task GetPackagingType_OnClicked(object sender, EventArgs e)
        {
            var page = new PackagingTypes();
            page.PackagingTypesListView.ItemSelected += (source, args) =>
            {
                var type = args.SelectedItem as PackagingType;
                if (type != null) PackagingType.Text = type.Name.ToString();
                Navigation.PopAsync();
            };

            await Navigation.PushAsync(page);
        }

        private async void TakePicture_Clicked(object sender, EventArgs e)
        {

            _mediafile = await _appFunctions.TakePicture();

            if (_mediafile == null)
                await DisplayAlert(null, "Could not take picture.", "OK");

            else
                Image.Source = _mediafile.Path;
        }

        private async void BrowsePicture_Clicked(object sender, EventArgs e)
        {
            _mediafile = await _appFunctions.BrowsePicture();

            if (_mediafile == null)
                await DisplayAlert(null, "Could not get picture.", "OK");

            else
                Image.Source = _mediafile.Path;
        }

        private async void Save_OnClicked(object sender, EventArgs e)
        {
            if (ProductType.Text.Equals(NoSelection) ||
                String.IsNullOrWhiteSpace(ProductDescription.Text) ||
                String.IsNullOrWhiteSpace(ProductCity.Text) ||
                String.IsNullOrWhiteSpace(PickupAddr.Text) ||
                Convert.ToDouble(ProductQuantity.Text) <= 0 ||
                PackagingType.Text.Equals(NoSelection) ||
                _mediafile == null)
                await DisplayAlert(null, "Please complete all the fields and set a product picture.", "Ok");

            else
            {
                loader.IsVisible = true;
                label.IsVisible = true;
                table.IsVisible = false;

                var product = new Product
                {
                    ProductType = ProductType.Text,
                    TransactionRating = ProductRating.Value,
                    Description = ProductDescription.Text,
                    Country = "South Africa",
                    City = ProductCity.Text,
                    AvailableHours = TimeFrom.Time.ToString(@"hh\:mm") + "-" + TimeTo.Time.ToString(@"hh\:mm"),
                    Quantity = Convert.ToDouble(ProductQuantity.Text),
                    PaymentMethods = "No payment required",
                    Packaging = PackagingType.Text,
                    ProductPicture = _mediafile.Path,
                    File = _mediafile,
                    Latitude = _latitude,
                    Longitude = _longitude,
                    MonToFridayHours = TimeFrom.Time.ToString(@"hh\:mm") + "-" + TimeTo.Time.ToString(@"hh\:mm"),
                    SaturdayHours = TimeFromSat.Time.ToString(@"hh\:mm") + "-" + TimeToSat.Time.ToString(@"hh\:mm"),
                    SundayHours = TimeFromSun.Time.ToString(@"hh\:mm") + "-" + TimeFromSun.Time.ToString(@"hh\:mm"),
                    PickUpAddress = PickupAddr.Text,
                    SellByDate = SellBy.Date
                };

                var response = await _service.AddNewPost(product);
                if (response)
                {
                    await DisplayAlert("Success", "New post created.", "Ok");
                    await Navigation.PopAsync();
                }

                else
                {
                    await DisplayAlert("Failed", "Failed to create post.", "Ok");
                    loader.IsVisible = false;
                    label.IsVisible = false;
                    table.IsVisible = true;
                }
            }
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
                    var results = await CrossGeolocator.Current.GetPositionAsync(10000);

                    // lat.Text = "Lat: " + results.Latitude + " Long: " + results.Longitude;
                    _latitude = results.Latitude.ToString();
                    _longitude = results.Longitude.ToString();
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                // longit.Text = "Error: " + ex;
            }
        }
    }
}