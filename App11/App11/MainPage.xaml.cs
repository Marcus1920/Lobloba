using App11.Views.auth;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            UserIcon.Source = ImageSource.FromFile("logo.png");
        }
        private async void Btnregister_Clicked(object sender, EventArgs e)
        {
               await Navigation.PushModalAsync(new  Registration());

            
        }

        private async void Camera_Clicked(object sendr, EventArgs e)

        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
              await  DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });
            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

        }




        private async void BtnLogin_clicked(object sender, EventArgs e)
        {

          //   await DisplayAlert("ok", "alll", "Ok");
             await Navigation.PushAsync(new Login());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void Maps_Clicked(object sender, EventArgs e)
        {
              await Navigation.PushAsync(new Map());

        
        }


    }
}