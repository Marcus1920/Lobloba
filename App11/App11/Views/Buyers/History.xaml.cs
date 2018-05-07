using App11.Models.BuyersModel;
using App11.Services.SellersServices;
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
	public partial class History : ContentPage
	{
        private ObservableCollection<HistoryTransaction> _transactions;
        HistoryData data = new HistoryData();
        private readonly TransactionsServiceBuyer _service = new TransactionsServiceBuyer();
        public History()
        {
            BindingContext = this;

            InitializeComponent();
        }

       

        private void ToolbarItem_OnActivatedChat(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChatList());
        }

        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserProductCart());
        }

        protected override async void OnAppearing()
        {
            /*var allProducts = await data.GetHistoryList();
            productsList = new ObservableCollection<HistoryTransaction>(allProducts);
            TransactionsListView.ItemsSource = productsList; */
            await GetTransactions();

            base.OnAppearing();

        }
        private async void TransactionsListView_OnRefreshing(object sender, EventArgs e)
        {
            await GetTransactions();

            TransactionsListView.EndRefresh();
        }

        async void OnHistoryTapped(object sender, ItemTappedEventArgs e)
        {

            HistoryTransaction history = (HistoryTransaction)e.Item;
            await Navigation.PushAsync(new RateSeller(history));
        }
        public async Task GetTransactions()
        {
            try
            {
                notFound.IsVisible = true;
                notFound.Text = "Loading...";

                var list = await _service.GetAllTransactions();

                _transactions = new ObservableCollection<HistoryTransaction>(list);
                TransactionsListView.ItemsSource = _transactions;

                TransactionsListView.IsVisible = _transactions.Any();
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