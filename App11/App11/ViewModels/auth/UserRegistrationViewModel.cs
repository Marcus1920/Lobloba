using Acr.UserDialogs;
using App11.Models;
using App11.Views.auth;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.ViewModels.auth
{
    public class UserRegistrationViewModel : INotifyPropertyChanged 
    {


        public Command RegistrationCommnd { get; }
        public Command OpenOtherPageCommand { get; }
        INavigation Navigation;
        public UserRegistrationViewModel()
        {

            RegistrationCommnd = new Command(SaveAsync);
            getcurentAsync();

        }

        public UserRegistrationViewModel(INavigation MainPageNav)

        {
            Navigation = MainPageNav;
            OpenOtherPageCommand = new Command(async () => await OpenOtherPage());

            RegistrationCommnd = new Command(SaveAsync);
            getcurentAsync();



        }
        public async Task OpenOtherPage()
        {
            await Navigation.PushModalAsync(new Login());
        }



        private String name = "";
        private String surname = "";
        private String cellphone = "";
        private String idNumber = "";
        private String email = "";
        private String intrest = "";
        private String traveleRadius = "";
        private String transport = "";
        private String location = "";
        private String apiLong;
        private String apiLat;
        private Boolean active = true;







        public event PropertyChangedEventHandler PropertyChanged;

        void OnpertyChanged([CallerMemberName] String name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        public String Name
        {
            get { return name; }
            set
            {

                name = value;
                if (name == "MT")
                {
                    Active = true;
                }
                else
                {
                    Active = false;
                }
                OnpertyChanged(nameof(Name));
                OnpertyChanged(nameof(Message));


            }

        }

        public Boolean Active

        {
            get { return active; }
            set
            {
                active = value;
                OnpertyChanged(nameof(Active));

            }
        }


        public String Intrest
        {
            get { return intrest; }
            set { intrest = value; }

        }
        public String ApiLong
        {
            get { return apiLong; }



        }
        public String ApiLat
        {
            get { return apiLat; }



        }
        public String Location
        {
            get { return location; }
            set { location = value; }
        }
        public String TraveleRadius
        {
            get { return traveleRadius; }
            set
            {

                traveleRadius = value;
                OnpertyChanged(nameof(TraveleRadius));
                OnpertyChanged(nameof(Message));
            }
        }
        public String Transport
        {
            get { return transport; }
            set
            {

                transport = value;
                OnpertyChanged(nameof(Transport));
                OnpertyChanged(nameof(Message));

            }
        }
        public String Surname
        {
            get { return surname; }
            set { surname = value; }

        }

        public String Cellphone
        {

            get { return cellphone; }

            set
            {

                cellphone = value;
                OnpertyChanged(nameof(Cellphone));
                OnpertyChanged(nameof(Message));
            }

        }
        public String IdNumber
        {

            get { return idNumber; }

            set { idNumber = value; }

        }
        public String Email
        {

            get { return email; }

            set
            {

                email = value;
                OnpertyChanged(nameof(Email));
                OnpertyChanged(nameof(Message));

            }

        }




        public String Message
        {
            get { return $"Your  Name  is " + Name + " **" + Surname + "**" + Cellphone + "Travale" + TraveleRadius + "GPS" + ApiLong + "LAT GSP" + ApiLat; }


        }

        async void SaveAsync()

        {


            try
            {

                UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);
                var values = new FormUrlEncodedContent(new[]
        {
                        new KeyValuePair<string, string>("name", Name),
                         new KeyValuePair<string, string>("location", Location),
                        new KeyValuePair<string,  string>("surname",Surname),
                        new KeyValuePair<string, string>("emails",Email),
                        new KeyValuePair<string, string>("intrest",Intrest),
                        new KeyValuePair<string, string>("travel_radius",traveleRadius),
                        new KeyValuePair<string, string>("description_of_acces",Transport),
                        new KeyValuePair<string, string>("cell",Cellphone),
                        new KeyValuePair<string, string>("IdNumber",IdNumber),
                        new KeyValuePair<string, string>("gps_lat",ApiLat),
                        new KeyValuePair<string, string>("gps_long",ApiLong)
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
                    UserDialogs.Instance.Alert("Notification", "   Your account has been successfully created. Please wait for approval, thank you. (4-48 hours)", "OK");

                    await OpenOtherPage();
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.Alert("Erro", responses.Erro, "OK");

                }
            }
            catch (Exception ex)
            {

                UserDialogs.Instance.Alert("Erro", ex + " Connection interrupted.Please check your network status, refresh the page or try again later.", "OK");
            }

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

                        UserDialogs.Instance.Alert("Notication", "Gunna need that location", "OK");
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
                    apiLong = results.Latitude.ToString();
                    apiLat = results.Longitude.ToString();
                }
                else if (status != PermissionStatus.Unknown)
                {


                    UserDialogs.Instance.Alert("Notication", "Can not continue, try again", "OK");
                }
            }
            catch (Exception ex)
            {
                // longit.Text = "Error: " + ex;
                UserDialogs.Instance.Alert("Notication", "Can not continue, try again" + ex, "OK");
            }
        }

    }
}

