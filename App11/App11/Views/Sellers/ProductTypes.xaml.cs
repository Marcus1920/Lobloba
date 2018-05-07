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
	public partial class ProductTypes : ContentPage
	{
        private ObservableCollection<ProductType> _types;
        private readonly ProductsService _service = new ProductsService();

        public ProductTypes()
        {
            BindingContext = this;

            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await GetTypes();

            base.OnAppearing();
        }

        private async void TypesListView_OnRefreshing(object sender, EventArgs e)
        {
            await GetTypes();

            TypesListView.EndRefresh();
        }

        public ListView ProductTypesListView => TypesListView;

        private async Task GetTypes(string searchString = null)
        {
            try
            {
                Indicator.IsVisible = true;
                notFound.IsVisible = true;
                notFound.Text = "Searching...";

                var typestList = await _service.GetProductTypes(searchString);

                _types = new ObservableCollection<ProductType>(typestList);

                TypesListView.ItemsSource = _types;

                TypesListView.IsVisible = _types.Any();
                notFound.IsVisible = !TypesListView.IsVisible;
            }
            catch (Exception)
            {
                await DisplayAlert("Error!",
                    "Connection interrupted. Please check your network status, refresh the page or try again later.",
                    "OK");
            }
            finally
            {
                Indicator.IsVisible = false;
                notFound.Text = "No items found.";
            }
        }

        private async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
                return;

            await GetTypes(e.NewTextValue);
        }
    }
}