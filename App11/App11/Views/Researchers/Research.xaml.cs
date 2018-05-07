using App11.Models;
using App11.Models.ResercherModel;
using App11.Views.Researchers.ResearchApi;
using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Researchers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Research : ContentPage
	{
        private MediaFile _Mediafile;
        private Research research;

        public Research()
        {
            InitializeComponent();
            UserIcon.Source = ImageSource.FromFile("logo.png");
        }

        public Research(Research research)
        {
            this.research = research;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Sorry", "Pick photo is not support !", "OK");
                return;
            }

            _Mediafile = await CrossMedia.Current.PickPhotoAsync();
            if (_Mediafile == null)
                return;



            PathLabel.Text = "Photo path" + _Mediafile.Path;

            //   MainImage.Aspect = Aspect.AspectFit;

            MainImage.Source = ImageSource.FromStream(() => {
                var stream = _Mediafile.GetStream();
                //  _Mediafile.Dispose();

                return _Mediafile.GetStream();


            });



        }

        private async void TakePicture_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable
                     || !CrossMedia.Current.IsPickPhotoSupported)

            {
                await DisplayAlert("No  camera  ", ":( No camera  Available", "OK");

                return;

            }

            _Mediafile = await CrossMedia.Current.TakePhotoAsync(

                 new StoreCameraMediaOptions
                 {


                     SaveToAlbum = true,
                     //  Directory = "SiyaleaderMobileBeta.BrowseImage",

                     //  Name = "HOM.PNG

                 });


            if (_Mediafile == null)
                return;


            PathLabel.Text = _Mediafile.Path;


            MainImage.Source = ImageSource.FromFile(_Mediafile.Path);


            /*    MainImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = _Mediafile.GetStream();
                    _Mediafile.Dispose();
                    return stream;


                });*/

        }


        public async Task GetProductType_OnItemSelected(object sender, EventArgs e)
        {
            var page = new Product();
            page.ProductTypesListView.ItemSelected += async (source, args) =>
            {
                var type = args.SelectedItem as ProductTypes;

                if (type != null)
                    productR.Text = type.Name.ToString();

                await Navigation.PopAsync();
            };

            await Navigation.PushAsync(page);

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
                    api_lat.Text = results.Latitude.ToString();
                    api_long.Text = results.Longitude.ToString();
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception)
            {
                // longit.Text = "Error: " + ex;
            }
        }

        private async void SaveResearch_Clicked(object sender, EventArgs e)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(_Mediafile.GetStream()), "\"file\"", $"\"{_Mediafile.Path}\"");
            var httpclient = new HttpClient();

            //var httresponse = await httpclient.PostAsync(url, content);
            //var ff = await httresponse.Content.ReadAsStringAsync();
            //DisplayAlert("Alert", ff.ToString(), "Ok");

            String natureOfBusiness = productR.Text;
            String summaryBox = Summary.Text;
            String researchNotes = Note.Text;
            string gps_lat = api_lat.Text;
            string gps_long = api_long.Text;

            string api_key = (Application.Current as App).UserTokenKey; ;



            var client = new HttpClient();
            var contents = new MultipartContent();
            var response = await client.PostAsync("http://154.0.164.72:8080/Foods/api/v1/createResearch?api_key=" + api_key + "&natureOfBusiness" + natureOfBusiness + "&summaryBox=" + summaryBox + "&researchNotes=" + researchNotes + "", content);
            var respond = await response.Content.ReadAsStringAsync();
            await DisplayAlert("Alert", "Thank you. Your research has been successful submit", "Ok");


            await Navigation.PushAsync(new ResearchList());


            //login responses = JsonConvert.DeserializeObject<login>(respond);

            productR.Text = null;
            Summary.Text = null;
            Note.Text = null;
            MainImage.Source = null;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //  getcurentAsync();

        }
    }
}