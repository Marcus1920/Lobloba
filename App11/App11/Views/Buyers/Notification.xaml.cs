using App11.Models;
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
	public partial class Notification : ContentPage
	{
        private ObservableCollection<NotificationMessageModel> _notificationMessage;

        Callbackdata NewCall = new Callbackdata();

        public Notification()
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


        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserProductCart());
        }


        async void OnHistoryTappedAsync(object sender, ItemTappedEventArgs e)
        {
            NotificationMessageModel notificationMessa = (NotificationMessageModel)e.Item;
            await Navigation.PushAsync(new NotificationDetails(notificationMessa));
        }

        async void TransactionsListView_OnRefreshingRefreshingAsync(object sender, EventArgs e)
        {

            await GetTransactions();

            TransactionsListView.EndRefresh();

        }


    }
}