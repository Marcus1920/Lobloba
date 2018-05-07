using App11.Models.ResercherModel;
using App11.Services.ResearchersServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Researchers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Product : ContentPage
	{
        private ObservableCollection<ProductTypes> _types;
        private readonly ProductTypesService _service = new ProductTypesService();
        public Product()
        {
            BindingContext = this;
            InitializeComponent();
        }
        public ListView ProductTypesListView => ProductsListView;



        private async void ProductsListView_OnRefreshing(object sender, EventArgs e)
        {
            await GetTypes();

            ProductsListView.EndRefresh();
        }

        protected override async void OnAppearing()
        {
            await GetTypes();

            base.OnAppearing();
        }

        public async Task GetTypes(string searchString = null)
        {
            try
            {
                Indicator.IsVisible = true;
                notFound.IsVisible = true;
                notFound.Text = "Searching...";

                var typestList = await _service.GetProductTypes(searchString);

                _types = new ObservableCollection<ProductTypes>(typestList);
                ProductsListView.ItemsSource = _types;

                ProductsListView.IsVisible = _types.Any();
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
                notFound.Text = "No items found.";
                Indicator.IsVisible = false;
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