using App11.Models;
using App11.Services.ShareService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Buyers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatList : ContentPage
	{
        private ObservableCollection<ChatListModel> _chatList;

        private readonly ChatListingService _service = new ChatListingService();
        public ChatList()
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

    }
}