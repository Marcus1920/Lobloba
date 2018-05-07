using Acr.UserDialogs;
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
	public partial class Changepassword : ContentPage
	{
        private readonly string apiKey = (Application.Current as App).UserTokenKey;
        public Changepassword()
        {
            InitializeComponent();
        }

        private async void btnChangepassword_Clicked(object sender, EventArgs e)
        {

            var OldPassword = Currentpassword.Text;
            var NewPassword = newpassword.Text;
            var CornfirmPasswod = confirmpassword.Text;
            if (
            String.IsNullOrWhiteSpace(Currentpassword.Text) ||
             String.IsNullOrWhiteSpace(newpassword.Text) ||
             String.IsNullOrWhiteSpace(confirmpassword.Text))
            {
                //   Message.Text = "";

                // await DisplayAlert(null, "Please complete all the fields ", "Ok");

                UserDialogs.Instance.ShowError("Please complete all the fields", 2000);

            }

            else
            {
                var api_key = apiKey;
                var client = new HttpClient();
                var contents = new MultipartContent();
                var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/changepassword?api_key=" + api_key + "&OldPassword=" + OldPassword + "&NewPassword=" + NewPassword + "&CornfirmPasswod=" + CornfirmPasswod + "", contents);
                var respond = await response.Content.ReadAsStringAsync();
                //  await DisplayAlert("Alert", respond , "Ok");
                String message = "Password successfuly changed";

                if (respond.Equals(message))
                {
                    UserDialogs.Instance.ShowSuccess(respond, 2000);
                }
                else
                {
                    UserDialogs.Instance.ShowSuccess(respond, 2000);
                }
                //    login responses = JsonConvert.DeserializeObject<login>(respond);
            }

        }
    }
}