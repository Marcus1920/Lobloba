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

namespace App11.Views.Researchers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResearcherMasterMaster : ContentPage
    {

        private readonly UserProfileService _service = new UserProfileService();
        public ListView ListView;

        public ResearcherMasterMaster()
        {
            InitializeComponent();

            BindingContext = new ResearcherMasterMasterViewModel();
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

        class ResearcherMasterMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<ResearcherMasterMenuItem> MenuItems { get; set; }

            public ResearcherMasterMasterViewModel()
            {
                MenuItems = new ObservableCollection<ResearcherMasterMenuItem>(new[]
                {

                      new ResearcherMasterMenuItem { Id = 0, Title = "View profile", TargetType = typeof(UserProfile) },
                 //   new ResearcherMasterMenuItem { Id = 1, Title = "Your posts", TargetType = typeof(ProductList) },
                      new ResearcherMasterMenuItem { Id = 2, Title = "Researcher", TargetType = typeof(ReasercherTab) },

                   //    new ResearcherMasterMenuItem { Id = 2, Title = "Research Feeds", TargetType = typeof(ResearchFeeds) },

                    
                //    new ResearcherMasterMenuItem { Id = 2, Title = "Donations", TargetType = typeof(DonationList) },
                 //   new ResearcherMasterMenuItem { Id = 3, Title = "Transactions", TargetType = typeof(TransactionList) },
                    //new SellersMasterMenuItem { Id = 5, Title = "Settings", TargetType = typeof(SellersMasterDetail) },
                    new ResearcherMasterMenuItem { Id = 1, Title = "Public Wall", TargetType = typeof(PublicWall) },
                       new ResearcherMasterMenuItem { Id = 2, Title = "Switch Account", TargetType = typeof(Shared.SwitshPage) },

                    //new SellersMasterMenuItem { Id = 7, Title = "Help", TargetType = typeof(SellersMasterDetail) },
                    new ResearcherMasterMenuItem { Id = 2, Title = "Change Password", TargetType = typeof(Shared.Changepassword) },
                    new ResearcherMasterMenuItem { Id = 2, Title = "Logout", TargetType = typeof(Login) }
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