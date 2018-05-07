using App11.Services.SellersServices;
using App11.Views.auth;
using App11.Views.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Buyers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuyersHomeMaster : ContentPage
	{
        private readonly UserProfileService _service = new UserProfileService();
        public ListView ListView;

        public BuyersHomeMaster()
        {
            InitializeComponent();

            BindingContext = new BuyersHomeMasterViewModel();
            ListView = MenuItemsListView;
        }
        protected override async void OnAppearing()
        {
            await GetUserProfile();

            base.OnAppearing();
        }

        public async Task GetUserProfile()
        {
            try
            {
                var profile = await _service.GetUser();

                UserName.Text = profile.Name;
                UserEmail.Text = profile.Email;
                ProfilePic.Source = profile.profilePicture;

            }
            catch (Exception)
            {
                await DisplayAlert("Error!",
                    "Connection interrupted. Please check your network status, refresh the page or try again later.",
                    "OK");

                await Navigation.PopAsync();
            }
        }
        class BuyersHomeMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<BuyersHomeMenuItem> MenuItems { get; set; }

            public BuyersHomeMasterViewModel()
            {
                MenuItems = new ObservableCollection<BuyersHomeMenuItem>(new[]
                {

                    new  BuyersHomeMenuItem { Id = 0, Title = "Home", IconSource="if_home_54119.png", TargetType = typeof(BuyersTabs) },
                    new  BuyersHomeMenuItem { Id = 0, Title = "View profile", IconSource="viewprofile.png", TargetType = typeof(UserProfile) },
                    //   new BuyersHomeMenuItem { Id = 3, Title = "Research", TargetType = typeof(BuyersHomeDetail) },
                    //  new BuyersHomeMenuItem { Id = 3, Title = "Settings", TargetType = typeof(BuyersHomeDetail) },
              
                    new BuyersHomeMenuItem { Id = 3, Title = "Donations",IconSource="donate.png" , TargetType = typeof(DonationList) },
                    //     new BuyersHomeMenuItem { Id = 3, Title = "Settings", TargetType = typeof(BuyersHomeDetail) },
                    new BuyersHomeMenuItem { Id = 3, Title = "Public Wall", IconSource="publicwall.png" , TargetType = typeof(PublicWall) },
                    new BuyersHomeMenuItem { Id = 2, Title = "Switch Account",IconSource="switchprofile.png" , TargetType = typeof(Shared.SwitshPage) },
                    new BuyersHomeMenuItem { Id = 2, Title = "Manage Alerts",IconSource="updateinterests.png" ,  TargetType = typeof(AlertManagertabuy) },
                    new BuyersHomeMenuItem { Id = 2, Title = "Change Password",IconSource="changepassword.png" , TargetType = typeof(Shared.Changepassword) },
                    //     new BuyersHomeMenuItem { Id = 3, Title = "Help", TargetType = typeof(BuyersHomeDetail) },
                    new BuyersHomeMenuItem { Id = 3, Title = "Logout",IconSource="logout.png" , TargetType = typeof(Login)}
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}