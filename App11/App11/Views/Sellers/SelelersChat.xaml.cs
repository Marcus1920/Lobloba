using App11.Models;
using App11.Services.ShareService;
using App11.Views.Buyers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Sellers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelelersChat : ContentPage
	{
        private ObservableCollection<ChatListModel> _chatList;

        private readonly ChatListingService _service = new ChatListingService();
        public SelelersChat()
        {
            InitializeComponent();
        }
        private void TransactionsListView_OnRefreshingtView_OnRefreshing(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void OnHistoryTappedAsync(object sender, ItemTappedEventArgs e)
        {

            ChatListModel Chatdetails = (ChatListModel)e.Item;

            await Navigation.PushAsync(new Chat(Chatdetails));


        }

        protected override async void OnAppearing()
        {
            await GetChatListing();
            base.OnAppearing();
        }


        public async Task GetChatListing()
        {
            try
            {
                notFound.IsVisible = true;
                notFound.Text = "Loading...";

                var list = await _service.GetChatListingdetails();

                _chatList = new ObservableCollection<ChatListModel>(list);
                TransactionsListView.ItemsSource = _chatList;

                TransactionsListView.IsVisible = _chatList.Any();
                notFound.IsVisible = !TransactionsListView.IsVisible;
            }
            catch (Exception)
            {
                await DisplayAlert("Error!",
                    "Connection interrupted. Please check your network status, refresh the page or try again later.",
                    "OK");
            }
            finally
            {
                notFound.Text = "No items found.";
            }
        }
        private async void ChangePickup_OnActivated(object sender, EventArgs e)
        {
            if (await DisplayAlert("Use current location?",
                "It is advisable that you visit the actual pick up point to update this information." +
                "Would you like to continue?",
                "Yes, Continue", "No"))
                await Navigation.PushAsync(new ChangeLocation());
        }

        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewProduct());
        }

    }
}