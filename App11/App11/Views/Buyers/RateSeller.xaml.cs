using App11.Models.BuyersModel;
using App11.Services.SellersServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Buyers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RateSeller : ContentPage
	{
        private readonly HistoryTransaction _transaction;
        private readonly TransactionsServiceBuyer _service = new TransactionsServiceBuyer();

        public RateSeller(HistoryTransaction transaction)
        {
            _transaction = transaction;

            BindingContext = _transaction ?? throw new ArgumentNullException();

            InitializeComponent();


        }

        protected override void OnAppearing()
        {
            SetStatus();

            base.OnAppearing();
        }

        public void SetStatus()
        {
            switch (_transaction.TransactionName)
            {
                case "New":
                    StatusLabel.Text = _transaction.TransactionName;
                    StatusLabel.TextColor = _transaction.StatusColor;
                    AcceptButton.IsVisible = true;
                    DeclineButton.IsVisible = true;
                    CloseButton.IsVisible = false;
                    break;

                case "Active":
                    StatusLabel.Text = _transaction.TransactionName;
                    StatusLabel.TextColor = _transaction.StatusColor;
                    CloseButton.IsVisible = true;
                    AcceptButton.IsVisible = false;
                    DeclineButton.IsVisible = false;
                    break;

                case "Cancelled":
                    StatusLabel.Text = _transaction.TransactionName;
                    StatusLabel.TextColor = _transaction.StatusColor;
                    AcceptButton.IsVisible = false;
                    DeclineButton.IsVisible = false;
                    CloseButton.IsVisible = false;
                    break;

                case "Declined":
                    StatusLabel.Text = _transaction.TransactionName;
                    StatusLabel.TextColor = _transaction.StatusColor;
                    StatusLabel.TextColor = Color.Red;
                    AcceptButton.IsVisible = false;
                    DeclineButton.IsVisible = false;
                    CloseButton.IsVisible = false;
                    break;

                case "Completed":
                    StatusLabel.Text = _transaction.TransactionName;
                    StatusLabel.TextColor = _transaction.StatusColor;
                    AcceptButton.IsVisible = false;
                    DeclineButton.IsVisible = false;
                    CloseButton.IsVisible = false;
                    break;
            }
        }

        private async void AcceptButton_OnClicked(object sender, EventArgs e)
        {

            /*  Loader.IsVisible = true;
              Details.IsVisible = false;

              var transaction = _transaction;
              transaction.AcceptOrder();
              var response = await _service.ChangeTransactionStatus(transaction);
              if (response)
              {
                  _transaction.AcceptOrder();
                  await DisplayAlert(null, "Transaction now Active.", "Ok");
                  SetStatus();
              }

              else
              {
                  await DisplayAlert("Error!", "Failed to communicate with the server. Please try again later", "Ok");
              }

              Loader.IsVisible = false;
              Details.IsVisible = true;
              */
        }

        private async void DeclineButton_OnClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Decline?", "Are you sure you want to reject this order?", "Yes", "No"))
            {
                Loader.IsVisible = true;
                Details.IsVisible = false;

                var transaction = _transaction;
                transaction.DeclineOrder();
                var response = await _service.ChangeTransactionStatus(transaction);
                if (response)
                {
                    _transaction.DeclineOrder();
                    await RateTransaction();
                    SetStatus();
                }

                else
                {
                    await DisplayAlert("Error!", "Failed to communicate with the server. Please try again later", "Ok");
                    Details.IsVisible = true;
                }

                Loader.IsVisible = false;
            }
        }

        private async void CloseButton_OnClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Close?", "Are you sure you want to close this transaction?", "Yes", "No"))
            {
                Loader.IsVisible = true;
                Details.IsVisible = false;

                var transaction = _transaction;
                transaction.CloseTransaction();
                var response = await _service.ChangeTransactionStatus(transaction);
                if (response)
                {
                    _transaction.CloseTransaction();
                    await RateTransaction();
                    SetStatus();
                }

                else
                {
                    await DisplayAlert("Error!", "Failed to communicate with the server. Please try again later", "Ok");
                    Details.IsVisible = false;
                }

                Loader.IsVisible = false;
            }
        }

        public async Task RateTransaction()
        {
            await DisplayAlert(null, "Would you sell to this customer again?", "Yes", "No");
            Comment.IsVisible = true;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            Comment.IsVisible = false;
            Loader.IsVisible = true;

            await DisplayAlert(null, "Thank you for your feedback.", "Ok");

            Loader.IsVisible = false;
            Details.IsVisible = true;
        }
    }
}