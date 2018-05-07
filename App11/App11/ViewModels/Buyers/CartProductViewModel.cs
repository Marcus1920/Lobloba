using Acr.UserDialogs;
using App11.Models.BuyersModel;
using App11.Views.Buyers;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.ViewModels.Buyers
{
    public class CartProductViewModel : INotifyPropertyChanged
    {
        public Command OpenOtherPageCommand { get; }
        INavigation Navigation;


        //  public ObservableCollection<CartProduct> usersCartList { get; set; }

        private List<CartProduct> usersCartList;




        public List<CartProduct> UsersCartList
        {
            get
            {
                return usersCartList;
            }

            set
            {
                usersCartList = value;
                OnpropertyChanged();

            }

        }
        public Command<CartProduct> RemoveComand

        {

            get

            {
                return new Command<CartProduct>(async (CartProduct) =>

                {

                    //UsersCartList.Remove(CartProduct);
                    string userApiKey = CrossSettings.Current.GetValueOrDefault("APIKey", "unknown");
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                        var values = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("api_key",userApiKey),
                        new KeyValuePair<string,string>("productType",CartProduct.productType.ToString()),
                        new KeyValuePair<string, string>("cartId",CartProduct.id),
                        new KeyValuePair<string, string>("sellerDetailsId",CartProduct.productId),

                    });

                        var client = new HttpClient();
                        var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/removeFromCart", values);
                        var respond = await response.Content.ReadAsStringAsync();
                        UserDialogs.Instance.HideLoading();


                        UserDialogs.Instance.ShowSuccess("Product was successfully Remove", 3000);

                        Debug.WriteLine("serveR:" + CartProduct.productId + "and " + CartProduct.id + "Productype" + CartProduct.productType + "apikey" + userApiKey);

                        await OpenOtherPage();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Iteme   fails");
                        //await DisplayAlert("Erro", " Connection interrupted.Please check your network status, refresh the page or try again later.", "OK");
                    }
                    finally
                    {

                        //  await DisplayAlert("Notification", "No Item found", "OK"); 
                    }



                });



            }


        }

        public CartProductViewModel()

        {
            InitializeDataAsync();



        }



        public CartProductViewModel(INavigation MainPageNav)

        {
            Navigation = MainPageNav;
            OpenOtherPageCommand = new Command(async () => await OpenOtherPage());

            InitializeDataAsync();



        }

        public async Task OpenOtherPage()
        {
            await Navigation.PushModalAsync(new BuyersHome());
        }



        private async Task InitializeDataAsync()
        {

            //  var userserve = new CaseServices();
            var CartProductDatas = new CartProductData();

            UsersCartList = await CartProductDatas.GetCartProducts();


        }



        public event PropertyChangedEventHandler PropertyChanged;



        protected virtual void OnpropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
