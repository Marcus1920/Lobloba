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

namespace App11.Views.Sellers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellersMasterMaster : ContentPage
    {
        private readonly UserProfileService _service = new UserProfileService();
        public ListView ListView;

        public SellersMasterMaster()
        {
            InitializeComponent();

            BindingContext = new SellersMasterMasterViewModel();
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

        class SellersMasterMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<SellersMasterMenuItem> MenuItems { get; set; }

            public SellersMasterMasterViewModel()
            {
                MenuItems = new ObservableCollection<SellersMasterMenuItem>(new[]
                {
                    // Commented stuff for next phase
                    new SellersMasterMenuItem { Id = 1, Title = "Home", IconSource="if_home_54119.png" , TargetType = typeof(SellersTabbedPage) },
                    new SellersMasterMenuItem { Id = 1, Title = "View profile", IconSource="viewprofile.png" , TargetType = typeof(UserProfile) },
                    //new SellersMasterMenuItem { Id = 2, Title = "History", TargetType = typeof(SellersMasterDetail) },
                    new SellersMasterMenuItem { Id = 2, Title = "My posts", IconSource="myposts.png" , TargetType = typeof(ProductList) },
                    new SellersMasterMenuItem { Id = 4, Title = "Donations",IconSource="donate.png" , TargetType = typeof(DonationList) },
                    new SellersMasterMenuItem { Id = 4, Title = "Transactions",IconSource="transactions.png" , TargetType = typeof(TransactionList) },
                    //new SellersMasterMenuItem { Id = 5, Title = "Settings", TargetType = typeof(SellersMasterDetail) },
                    new SellersMasterMenuItem { Id = 5, Title = "Public Wall",IconSource="publicwall.png" , TargetType = typeof(PublicWall) },
                    new SellersMasterMenuItem { Id = 6, Title = "Switch Account",IconSource="switchprofile.png" , TargetType = typeof(Shared.SwitshPage) },
                    new SellersMasterMenuItem { Id = 9, Title = "Manage Alerts",IconSource="updateinterests.png" , TargetType = typeof(AlertmanagerTab) },
                    new SellersMasterMenuItem { Id = 7, Title = "Change Password",IconSource="changepassword.png" , TargetType = typeof(Shared.Changepassword) },
                    new SellersMasterMenuItem { Id = 8, Title = "Logout",IconSource="logout.png" , TargetType = typeof(Login) }
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