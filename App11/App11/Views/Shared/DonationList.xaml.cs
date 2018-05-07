
using App11.Models.SellersModel;
using App11.Services.SellersServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DonationList : ContentPage
	{
        private ObservableCollection<Product> _posts;
        private readonly SellersPostsService _service = new SellersPostsService();

        public DonationList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await GetPosts();

            base.OnAppearing();
        }

        private async void ProductsListView_OnRefreshing(object sender, EventArgs e)
        {
            await GetPosts();

            ProductsListView.EndRefresh();
        }


        private async Task ProductsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var product = e.SelectedItem as Product;
            await Navigation.PushAsync(new DonationDetails(product));

            ProductsListView.SelectedItem = null;
        }

        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewDonation());
        }

        public async Task GetPosts()
        {
            try
            {
                notFound.IsVisible = true;
                notFound.Text = "Loading...";

                var poststList = await _service.GetAllDonations();

                _posts = new ObservableCollection<Product>(poststList);
                ProductsListView.ItemsSource = _posts;

                ProductsListView.IsVisible = _posts.Any();
                notFound.IsVisible = !ProductsListView.IsVisible;
            }
            catch (Exception)
            {
                await DisplayAlert("Error!",
                    "Connection interrupted. Please check your network status, refresh the page or try again later.",
                    "OK");
            }
            finally
            {
                notFound.Text = "No donations found.";
            }
        }
    }
}