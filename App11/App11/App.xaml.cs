using Com.OneSignal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App11
{
	public partial class App : Application
	{
        private const string TokenKey = "";
        public App ()
		{
			InitializeComponent();

		
            MainPage = new NavigationPage(new App11.MainPage() ); 
            OneSignal.Current.StartInit("9967977c-2282-4ff9-b208-2a59959a5359").EndInit();
        }

        public string UserTokenKey
        {
            get => Properties[TokenKey].ToString();
            set => Properties[TokenKey] = value;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
