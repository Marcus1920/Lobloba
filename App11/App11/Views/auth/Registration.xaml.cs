using Acr.UserDialogs;
using App11.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Geolocator;
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

namespace App11.Views.auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registration : ContentPage
	{
		public Registration ()
		{
			InitializeComponent ();
		}
        private async void Go()
        {
            await Navigation.PushAsync(new Login());
            UserIcon.Source = ImageSource.FromFile("logo.png");

        }



        private async void getcurentAsync()
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




        private async void register_Clicked(object sender, EventArgs e)
        {


            string intrest = Interests.SelectedItem.ToString();
            string locate = location.Text;
            string Traveradius = Travelradius.SelectedItem.ToString();
            string descriptios = Description.SelectedItem.ToString();
            string gps_lat = api_lat.Text;
            string gps_long = api_long.Text;

            try
            {




                if (
                   String.IsNullOrWhiteSpace(surname.Text) ||
                   String.IsNullOrWhiteSpace(name.Text) ||
                   String.IsNullOrWhiteSpace(id_no.Text) ||
                   String.IsNullOrWhiteSpace(Cellphone.Text) ||
                   String.IsNullOrWhiteSpace(email.Text))
                {
                    ///  Message.Text = "Please complete all the fields ";

                    //   await DisplayAlert(null, "Please complete all the fields ", "Ok");
                    UserDialogs.Instance.ShowSuccess("Please complete all the fields", 3000);


                }
                else
                {



                    var IsConnected = CrossConnectivity.Current.IsConnected;

                    if (IsConnected == false)
                    {

                        // await DisplayAlert(null, "Faild  to Connect  ", "Ok");

                        UserDialogs.Instance.ShowLoading("Please Check your Mobile Data Connection", MaskType.Black);
                    }



                    try
                    {

                        UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);
                        var values = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("name", name.Text),
                         new KeyValuePair<string, string>("location", location.Text),
                        new KeyValuePair<string,  string>("surname",surname.Text),
                        new KeyValuePair<string, string>("emails",email.Text),
                        new KeyValuePair<string, string>("intrest",Interests.SelectedItem.ToString()),
                        new KeyValuePair<string, string>("travel_radius",Travelradius.SelectedItem.ToString()),
                        new KeyValuePair<string, string>("description_of_acces",Description.SelectedItem.ToString()),
                        new KeyValuePair<string, string>("cell",Cellphone.Text),
                        new KeyValuePair<string, string>("IdNumber",id_no.Text),
                        new KeyValuePair<string, string>("gps_lat",api_lat.Text),
                        new KeyValuePair<string, string>("gps_long",api_long.Text)
                    });

                        var client = new HttpClient();
                        var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/register", values);
                        var respond = await response.Content.ReadAsStringAsync();

                        RegistrationModel  responses = JsonConvert.DeserializeObject<RegistrationModel>(respond);

                        //  await DisplayAlert("Notification", respond, "Ok");
                        // await Navigation.PushModalAsync(new BuyersHome());



                        if (responses.mesg == "Ok")
                        {
                            UserDialogs.Instance.HideLoading();
                            UserDialogs.Instance.ShowSuccess("Notification   Your account has been successfully created. Please wait for approval, thank you. (4-48 hours)", 3000);

                            name.Text = "";
                            surname.Text = "";
                            email.Text = "";
                            Cellphone.Text = "";
                            id_no.Text = "";
                            location.Text = ""; 



                             // await Navigation.PushAsync(new Login()); 

                            // Go(); 
                        }
                        else
                        {

                            await DisplayAlert("Erro", responses.Erro, "OK");
                            UserDialogs.Instance.HideLoading();

                        }
                    }
                    catch (Exception ex)
                    {

                        await DisplayAlert("Erro", ex + " Connection interrupted.Please check your network status, refresh the page or try again later.", "OK");
                    }
                    finally
                    {

                        // await DisplayAlert("Notification", "No Item found", "OK");
                    }


                }



            }

            catch (Exception ex)
            {

                await DisplayAlert("Erro", "" + ex, "OK");

            }





        }

        private void getLocation_Clicked(object sender, EventArgs e)
        {

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            getcurentAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            await UserIcon.TranslateTo(0, 200, 2000, Easing.BounceIn);

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void termes_Clicked(object sender, EventArgs e)
        {
            if (Device.OS != TargetPlatform.WinPhone)
            {
                Device.OpenUri(new Uri("http://foodforus.cloud/terms"));
            }
            else
            {
                DisplayAlert("To Do", "Not implemented yet", "OK");
            }
        }
    }
}