using Acr.UserDialogs;
using App11.Models;
using Newtonsoft.Json;

using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace App11.ViewModels
{
    public class UpdateIntresatViewModel : INotifyPropertyChanged
    {
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;
        public Command UpdateCommand { get;  }


        INavigation Navigation;
      
        private String _userIstresName = ""; 
        public UpdateIntresatViewModel()
        {

            UpdateCommand = new Command(SaveAsync);
            

        }
       
        public event PropertyChangedEventHandler PropertyChanged;
        void OnpertyChanged([CallerMemberName] String name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        public String ProductType
        {
            get { return _userIstresName; }
            set
            {
                _userIstresName = value;


                OnpertyChanged(nameof(ProductType));

            }
            

    }

        async void SaveAsync()

        {
          //  UserDialogs.Instance.ShowLoading("Loading ...", MaskType.Black);
            UserDialogs.Instance.Alert(null, "rr" + ProductType, "OK");
             Debug.WriteLine("Valus" + ProductType);
            
            /*  try
              {
  
                
                  var values = new FormUrlEncodedContent(new[]
             {
                       
  
              new KeyValuePair<string, string>("apiKey", _apiKey),
              new KeyValuePair<string, string>("userIntrest", userIstresName),
  
                      });
  
                  var client = new HttpClient();
                  var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/register", values);
                  var respond = await response.Content.ReadAsStringAsync();
  
                  Registre responses = JsonConvert.DeserializeObject<Registre>(respond);
  
                  //  await DisplayAlert("Notification", respond, "Ok");
                  // await Navigation.PushModalAsync(new BuyersHome());
  
  
  
                  if (responses.mesg == "Ok")
                  {
                      UserDialogs.Instance.HideLoading();
                      UserDialogs.Instance.Alert("Notification", "   Your account has been successfully created. Please wait for approval, thank you. (4-48 hours)", "OK");
  
               
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
              }*/

        }
    }
}
