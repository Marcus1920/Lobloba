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

namespace App11.Views.Sellers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductList : ContentPage
	{
        private ObservableCollection<Product> _posts;
        private readonly SellersPostsService _service = new SellersPostsService();

        public ProductList()
        {
            BindingContext = this;

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
            await Navigation.PushAsync(new ProductDetails(product));

            ProductsListView.SelectedItem = null;
        }

        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewProduct());
        }

        public async Task GetPosts()
        {
            try
            {
                notFound.IsVisible = true;
                notFound.Text = "Loading...";

                var poststList = await _service.GetAllPosts();

                _posts = new ObservableCollection<Product>(poststList);
                ProductsListView.ItemsSource = _posts;

                ProductsListView.IsVisible = _posts.Any();
                notFound.IsVisible = !ProductsListView.IsVisible;
            }
            catch (Exception)
            {
                await DisplayAlert("Connection Error!",
                    "Please check your network status, refresh the page (pull down) or try again later.",
                    "OK");
            }
            finally
            {
                notFound.Text = "No posts found.";
            }
        }

        private async void ChangePickup_OnActivated(object sender, EventArgs e)
        {
            if (await DisplayAlert("Use current location?",
                "It is advisable that you visit the actual pick up point to update this information." +
                "Would you like to continue?",
                "Yes, Continue", "No"))
                await Navigation.PushAsync(new ChangeLocation());
        }
    }
}