using Acr.UserDialogs;
using App11.Models;
using App11.ViewModels.auth;
using App11.Views.Buyers;
using App11.Views.Sellers;
using Com.OneSignal;
using Newtonsoft.Json;
using Plugin.LocalNotifications;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        LoginViewModel _loginViewModel;
        public Login()
        {
            InitializeComponent();
            _loginViewModel = new LoginViewModel();
            BindingContext = this._loginViewModel;



            UserIcon.Source = ImageSource.FromFile("logo.png");
            emails.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                btnLogin_cliked(sender, e);
            };

        }


        private void validation()
        {




        }

        private async void btnLogin_cliked(object sender, EventArgs e)
        {

            string email = emails.Text;
            string pass = Password.Text;


            if (
                 String.IsNullOrWhiteSpace(emails.Text) ||
                 String.IsNullOrWhiteSpace(Password.Text))

            {


                UserDialogs.Instance.ShowError("Please complete all the fields", 2000);


                //    await DisplayAlert(null, "Please complete all the fields ", "Ok");

            }
            else
            {
                UserDialogs.Instance.ShowLoading("Login...", MaskType.Gradient);
                login.Text = "Please wait...";
                var client = new HttpClient();
                var contents = new MultipartContent();
                var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/login?emails=" + email + "&password=" + pass + "", contents);
                var respond = await response.Content.ReadAsStringAsync();
                //   await DisplayAlert("Alert", respond , "Ok");
                loginModel responses = JsonConvert.DeserializeObject<loginModel>(respond);


                CrossLocalNotifications.Current.Show("FoodForus ", "Hi  " + " " + responses.name + ".  You have successfully  logged in ", 101, DateTime.Now.AddSeconds(5));
               

                if (responses.msg == "ok")
                {





                    UserDialogs.Instance.HideLoading();
                    CrossSettings.Current.AddOrUpdateValue("userlogin", "true");
                    login.Text = "Login";
                    Message.Text = "";
                    login.BackgroundColor = Color.FromHex("6f7376");
                    var app = Application.Current as App;
                    app.UserTokenKey = responses.apiKey;



                    post(); 

                    if (responses.intrest == "Seller")
                    {
                        CrossSettings.Current.AddOrUpdateValue("userrole", "Seller");
                        //  await Navigation.PushModalAsync(new SellersMaster());
                        await Navigation.PushModalAsync(new SellersMaster());
                    }

                    if (responses.intrest == "Buyer")
                    {
                        CrossSettings.Current.AddOrUpdateValue("userrole", "Buyer");
                        CrossSettings.Current.AddOrUpdateValue("APIKey", responses.apiKey);
                          await Navigation.PushModalAsync(new BuyersHome());

                        ///await DisplayAlert("ok", "sfsdfds", "okad"); 

                    }

                    if (responses.intrest == "Researcher")
                    {
                        CrossSettings.Current.AddOrUpdateValue("userrole", "Researcher");
                        //     await Navigation.PushModalAsync(new ResearcherMaster());

                        await DisplayAlert("ok", "sfsdfds", "okad");
                        //await Navigation.PushModalAsync(new BuyersHome());
                    }


                }
                else if (responses.msg == "notactive")
                {

                    UserDialogs.Instance.ShowError("Your account is not active", 3000);

                }

                else
                {
                    UserDialogs.Instance.ShowError("Invalid username or password", 3000);


                }
            }
        }


        private async void post()
        {
            try
            {
                string  ApiKey = (Application.Current as App).UserTokenKey;
                string id = "";
                OneSignal.Current.IdsAvailable((playerID, pushToken) =>
                {
                    id = playerID;
                    // App.Current.MainPage.DisplayAlert("playerId", id.ToString(), "OK");
                });

                string playerId = id;
                string api_key = ApiKey; 
                var client = new HttpClient();
                var contents = new MultipartContent();
                var response = await client.PostAsync("http://dev.foodforus.cloud/public/api/v1/updatePlayeId?api_key=" + api_key + "&playerID=" + playerId, contents);
                var respond = await response.Content.ReadAsStringAsync();

             

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hello", ex);
            }
        }
        private async void forgepassword_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new Forgotpassword());
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void emails_TextChanged(object sender, TextChangedEventArgs e)
        {




        }
    }
}