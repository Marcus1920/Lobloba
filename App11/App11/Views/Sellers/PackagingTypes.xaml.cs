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
	public partial class PackagingTypes : ContentPage
	{
        private ObservableCollection<PackagingType> _types;
        private readonly ProductsService _service = new ProductsService();

        public PackagingTypes()
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

        public ListView PackagingTypesListView => TypesListView;

        public async Task GetTypes()
        {
            try
            {
                notFound.IsVisible = true;
                notFound.Text = "Loading...";

                var typestList = await _service.GetPackagingTypes();

                _types = new ObservableCollection<PackagingType>(typestList);
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
                notFound.Text = "No items found.";
            }
        }
    }
}