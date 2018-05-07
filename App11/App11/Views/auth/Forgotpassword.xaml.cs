using App11.Models;
using Newtonsoft.Json;
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
	public partial class Forgotpassword : ContentPage
	{
		public Forgotpassword ()
		{
			InitializeComponent ();
		}
        private async void resetpasswpord_Clicked(object sender, EventArgs e)
        {
            string email = Email.Text;
            var client = new HttpClient();
            var contents = new MultipartContent();
            var httpset = await client.PostAsync("http://system.foodforus.cloud/api/v1/resetpassword?emails=" + email + "", contents);
            var response = await httpset.Content.ReadAsStringAsync();
            ResetpasswordModel responses = JsonConvert.DeserializeObject<ResetpasswordModel>(response);

            if (responses.message == "You have successfully change your password check your email")
            {
                //var label = new Label { Text = "This is a label.", TextColor = Color.FromHex("#77d065"), FontSize = 20 };
                error.TextColor = Color.FromHex("#008000");
                error.Text = responses.message;

            }
            else
            {
                error.TextColor = Color.FromHex("#008000");
                error.Text = responses.message;


            }
        }
    }
}