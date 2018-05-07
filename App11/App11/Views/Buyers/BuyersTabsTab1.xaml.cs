using App11.Models.BuyersModel;
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
	public partial class BuyersTabsTab1 : ContentPage
	{
        private ObservableCollection<Product> productsList;
        Data data = new Data();
        public BuyersTabsTab1()
        {
            InitializeComponent();

        }

        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserProductCart());
        }




        protected override async void OnAppearing()
        {

            var allProducts = await data.GetProducts();
            productsList = new ObservableCollection<Product>(allProducts);
            products.ItemsSource = productsList;
            base.OnAppearing();
        }




        async void OnTapped(object sender, ItemTappedEventArgs e)
        {
            Product product = (Product)e.Item;
            await Navigation.PushAsync(new ProductDetails(product));
        }

        private void ToolbarItem_OnActivatedChat(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChatList());
        }
    }
}