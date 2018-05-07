
using App11.Models.SellersModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DonationDetails : ContentPage
	{
        public DonationDetails(Product product)
        {
            BindingContext = product ?? throw new ArgumentNullException();

            InitializeComponent();
        }

        private async Task Button_OnClicked(object sender, EventArgs e)
        {
            await DisplayAlert(null, " functionality coming soon!", "Ok");
        }
    }
}