using Acr.UserDialogs;
using App11.Helpers;
using Com.OneSignal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace App11.ViewModels.auth
{
    class LoginViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;
        void OnpertyChanged([CallerMemberName] String name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }


        public Command LoginCommand { get; }


        public LoginViewModel()
        {

            LoginCommand = new Command(post);
        }



        private String email = Settings.LatEmailSettings;
        private String password = Settings.LatPasswordSettings;
        private String apiKey = Settings.LastApiKeySetting;



        public String Email
        {

            get { return email; }

            set
            {

                email = value;
                OnpertyChanged(nameof(Email));
                Settings.LatEmailSettings = value;



            }

        }


        public String Password
        {

            get { return password; }

            set
            {

                password = value;
                OnpertyChanged(nameof(Password));
                Settings.LatPasswordSettings = value;

            }

        }
        public String APIKEY
        {

            get { return apiKey; }

            set
            {

                apiKey = value;
                OnpertyChanged(nameof(APIKEY));
                Settings.LastApiKeySetting = value;



            }

        }


        void AsyncLogin()
        {


            UserDialogs.Instance.Alert(null, "Email : " + email + "Password" + password, "ok");
        }



        private async void post()
        {
            try
            {
                 string id = "";
                 OneSignal.Current.IdsAvailable((playerID, pushToken) =>
                 {
                     id = playerID;
                     // App.Current.MainPage.DisplayAlert("playerId", id.ToString(), "OK");
                 });
                 
                string playerId = id;
                string api_key = Settings.LastApiKeySetting;
                var client = new HttpClient();
                var contents = new MultipartContent();
                var response = await client.PostAsync("http://dev.foodforus.cloud/public/api/v1/updatePlayeId?api_key=" + api_key + "&playerID=" + playerId, contents);
                var respond = await response.Content.ReadAsStringAsync();

                UserDialogs.Instance.Alert(null, "Email" + apiKey);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hello", ex);
            }
        }

    }
}
