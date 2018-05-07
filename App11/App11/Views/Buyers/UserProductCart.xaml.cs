using Acr.UserDialogs;
using App11.Models.BuyersModel;
using App11.ViewModels.Buyers;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Buyers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProductCart : ContentPage
	{
        public UserProductCart()
        {

            InitializeComponent();
            BindingContext = new CartProductViewModel(Navigation);
        }

        async void usersCart_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CartProduct product = (CartProduct)e.Item;

            var annswer = await DisplayAlert("Buy Product", "Do you want to buy this product?", "Buy", "Cancel");

            if (annswer)
            {
                string userApiKey = CrossSettings.Current.GetValueOrDefault("APIKey", "unknown");
                string ApiKey = (Application.Current as App).UserTokenKey;
                //usersCart.BackgroundColor = Color.White;

                try
                {
                    var values = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("api_key",ApiKey),
                        new KeyValuePair<string,string>("productType",product.productType.ToString()),
                        new KeyValuePair<string, string>("cartId",product.id),
                        new KeyValuePair<string, string>("new_user_id",product.new_user_id),
                        new KeyValuePair<string, string>("productId",product.productId)
                    });

                    var client = new HttpClient();
                    var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/buy", values);
                    var respond = await response.Content.ReadAsStringAsync();



                    //   await DisplayAlert("", "Product Successfully Bought", "Ok");

                    UserDialogs.Instance.ShowSuccess("Product Successfully Bought", 3000);
                    await Navigation.PushModalAsync(new BuyersHome());
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Erro", " Connection interrupted.Please check your network status, refresh the page or try again later.", "OK");
                }
                finally
                {


                }

            }


            //await Navigation.PushModalAsync(new BuySaveProduct(product));
        }

        /*
         * 
         * 
         *    protected override async void OnAppearing()
           {
               var allProducts = await data.GetCartProducts();
               usersCartList = new ObservableCollection<CartProduct>(allProducts);
               usersCart.ItemsSource = usersCartList;
               base.OnAppearing();
           }
         * 
         * 
         * */





        private void Remove_Clicked_1(object sender, EventArgs e)
        {
            var Remove_Clicked = sender as Button;
            var CartProduc = Remove_Clicked?.BindingContext as CartProduct;

            var vm = BindingContext as CartProductViewModel;

            vm.RemoveComand.Execute(CartProduc);
        }
    }
}
