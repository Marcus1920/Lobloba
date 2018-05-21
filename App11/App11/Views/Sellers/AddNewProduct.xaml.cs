using Acr.UserDialogs;
using App11.Models.SellersModel;
using App11.Services.SellersServices;
using App11.ViewModels.Logic;
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

namespace App11.Views.Sellers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewProduct : ContentPage
	{
        private readonly SellersPostsService _service = new SellersPostsService();
        private readonly AppFunctions _appFunctions = new AppFunctions();
        private MediaFile _mediafile;
        private const string NoSelection = "No selection made.";
        private string _latitude;
        private string _longitude;
        private bool _locationError;


        public AddNewProduct()
        {
            InitializeComponent();

            ProductType.Text = NoSelection;
            PackagingType.Text = NoSelection;
            SellBy.MinimumDate = DateTime.Now;
            GetcurentAsync();
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
            UserDialogs.Instance.ShowLoading("Loading.....", MaskType.Black);
            if (ProductType.Text.Equals(NoSelection) ||
                String.IsNullOrWhiteSpace(ProductDescription.Text) ||
                String.IsNullOrWhiteSpace(ProductCity.Text) ||
                //String.IsNullOrWhiteSpace(PickupAddr.Text) ||
                Convert.ToDouble(ProductQuantity.Text) <= 0 ||
                Payment.SelectedIndex < 0 ||
                Convert.ToDouble(ProductPrice.Text) <= 0 ||
                PackagingType.Text.Equals(NoSelection) ||
                _mediafile == null)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert(null, "Please complete all the fields and set a product picture.", "Ok");
       
            }
                
         
            else
            {
                if (Pickup.SelectedIndex == 0)
                {
                    SetDefaultLocation();
                }

                if (Pickup.SelectedIndex == 1)
                {

                    SetDefaultLocation();

                }
              
                var product = new Product
                {
                    ProductType = ProductType.Text,
                    TransactionRating = ProductRating.Value,
                    Description = ProductDescription.Text,
                    Country = "South Africa",
                    City = ProductCity.Text,
                    AvailableHours = TimeFrom.Time.ToString(@"hh\:mm") + "-" + TimeTo.Time.ToString(@"hh\:mm"),
                    Quantity = Convert.ToDouble(ProductQuantity.Text),
                    CostPerKg = Convert.ToDouble(ProductPrice.Text),
                    Packaging = PackagingType.Text,
                    ProductPicture = _mediafile.Path,
                    File = _mediafile,
                    Latitude = _latitude,
                    Longitude = _longitude,
                    PaymentMethods = Payment.Items[Payment.SelectedIndex],
                    MonToFridayHours = TimeFrom.Time.ToString(@"hh\:mm") + "-" + TimeTo.Time.ToString(@"hh\:mm"),
                    SaturdayHours = TimeFromSat.Time.ToString(@"hh\:mm") + "-" + TimeToSat.Time.ToString(@"hh\:mm"),
                    SundayHours = TimeFromSun.Time.ToString(@"hh\:mm") + "-" + TimeFromSun.Time.ToString(@"hh\:mm"),
                    PickUpAddress = /*PickupAddr.Text*/ "Not specified",
                    SellByDate = SellBy.Date
                };
               
                var response = await _service.AddNewPost(product);
                if (response)
                {
                    UserDialogs.Instance.HideLoading();
                    //  await DisplayAlert("Success", "New post created.", "Ok");
                    UserDialogs.Instance.ShowSuccess("New post created.", 3000);
                    await Navigation.PopAsync();
                }

                else
                {
                   // UserDialogs.Instance.HideLoading();
                    // await DisplayAlert("Failed", "Failed to create post.", "Ok");
                    //UserDialogs.Instance.ShowError("Failed to create post.", 3000);
                    UserDialogs.Instance.ShowSuccess("New post created.", 3000);
                    await Navigation.PopAsync();
                }
            }
        }

        private void SetDefaultLocation()
        {
            _latitude = "0";
            _longitude = "0";
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
                _locationError = true;
            }
        }
    }
}