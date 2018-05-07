using App11.Models;
using App11.ViewModels;
using App11.Views.Sellers;
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
	public partial class EditAlertMangerbuy : ContentPage
	{
        private ObservableCollection<AlertproductModel> _alertproduct;

        CallbackdataAlert newServecal = new CallbackdataAlert();
        public EditAlertMangerbuy()
        {


            InitializeComponent();

            BindingContext = new AlertNotificationBuyerViewModel(Navigation);
        }
        protected override async void OnAppearing()
        {
            /* var allProducts = await NewCall.GetNotificationMessage();
             _notificationMessage = new ObservableCollection<NotificationMessageModel>(allProducts);
             TransactionsListView.ItemsSource = _notificationMessage; */
            await GetTransactions();

            base.OnAppearing();

        }

        public async Task GetTransactions()
        {
            try
            {
                notFound.IsVisible = true;
                notFound.Text = "Loading...";

                var list = await newServecal.GetalertIntrestList();

                _alertproduct = new ObservableCollection<AlertproductModel>(list);
                TransactionsListView.ItemsSource = _alertproduct;

                TransactionsListView.IsVisible = _alertproduct.Any();
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

        private void TransactionsListView_OnRefreshingRefreshing(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        async void OnHistoryTappedAsync(object sender, ItemTappedEventArgs e)
        {
            AlertproductModel notificationMessa = (AlertproductModel)e.Item;
            // await Navigation.PushAsync(new AlertproductModel(notificationMessa));
        }

        private async void TransactionsListView_OnRefreshing_OnRefreshingRefreshingAsync(object sender, EventArgs e)
        {
            await GetTransactions();

            TransactionsListView.EndRefresh();
        }


        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewProduct());
        }



        private async void ChangePickup_OnActivated(object sender, EventArgs e)
        {
            if (await DisplayAlert("Use current location?",
                "It is advisable that you visit the actual pick up point to update this information." +
                "Would you like to continue?",
                "Yes, Continue", "No"))
                await Navigation.PushAsync(new ChangeLocation());
        }


        private void Remove_OnClicked(object sender, EventArgs e)
        {

            var Remove_Clicked = sender as Button;
            var CartProduc = Remove_Clicked?.BindingContext as AlertproductModel;

            var vm = BindingContext as AlertNotificationBuyerViewModel;

            vm.RemoveComand.Execute(CartProduc);

        }


        private async void TransactionsListView_OnRefreshingAsync(object sender, EventArgs e)
        {
            await GetTransactions();

            TransactionsListView.EndRefresh();
        }
    }
}