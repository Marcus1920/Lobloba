using App11.Models;
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
	public partial class Notificationsel : ContentPage
	{
        private ObservableCollection<NotificationMessageModel> _notificationMessage;

        Callbackdata NewCall = new Callbackdata();
        public Notificationsel()
        {
            BindingContext = this;
            InitializeComponent();
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

                var list = await NewCall.GetNotificationMessage();

                _notificationMessage = new ObservableCollection<NotificationMessageModel>(list);
                TransactionsListView.ItemsSource = _notificationMessage;

                TransactionsListView.IsVisible = _notificationMessage.Any();
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
            NotificationMessageModel notificationMessa = (NotificationMessageModel)e.Item;
            await Navigation.PushAsync(new NotificationDetaislSel(notificationMessa));
        }

        private async void TransactionsListView_OnRefreshing_OnRefreshingRefreshingAsync(object sender, EventArgs e)
        {
            await GetTransactions();

            TransactionsListView.EndRefresh();
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