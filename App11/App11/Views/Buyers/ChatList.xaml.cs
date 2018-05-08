using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11.Models;
using App11.Models.BuyersModel;
using App11.Services.SellersServices;
using App11.Services.ShareService;
using App11.ViewModels;
using App11.ViewModels.Buyers;
using Food__For__us.ViewModels.Buyers;
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
            BindingContext = new DeleteChatViewModel(Navigation);
        }

        private async void TransactionsListView_OnRefreshingtView_OnRefreshing(object sender, EventArgs e)
        {
            await GetChatListing();

            TransactionsListView.EndRefresh();


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

        private void Remove_OnClickedcked_1(object sender, EventArgs e)
        {
            var Remove_Clicked = sender as Button;
            var CartProduc = Remove_Clicked?.BindingContext as ChatListModel;

            var vm = BindingContext as DeleteChatViewModel;

            vm.RemoveComand.Execute(CartProduc);
        }
    }
}