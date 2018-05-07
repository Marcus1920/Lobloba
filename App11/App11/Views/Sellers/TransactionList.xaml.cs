

using System;
using System.Collections.Generic;
using App11.Models.SellersModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using App11.Services.SellersServices;

namespace App11.Views.Sellers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransactionList : ContentPage
	{
        private ObservableCollection<Models.SellersModel.Transaction> _transactions;
        private readonly TransactionsService _service = new TransactionsService();

        public TransactionList()
        {
            BindingContext = this;

            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await GetTransactions();

            base.OnAppearing();
        }

        private async void TransactionsListView_OnRefreshing(object sender, EventArgs e)
        {
            await GetTransactions();

            TransactionsListView.EndRefresh();
        }

        private async Task TransactionsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var transaction = e.SelectedItem as Models.SellersModel.Transaction;
            await Navigation.PushAsync(new TransactionDetails(transaction));

            TransactionsListView.SelectedItem = null;
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
        public async Task GetTransactions()
        {
            try
            {
                notFound.IsVisible = true;
                notFound.Text = "Loading...";

                var list = await _service.GetAllTransactions();

                _transactions = new ObservableCollection<Models.SellersModel.Transaction>(list);
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