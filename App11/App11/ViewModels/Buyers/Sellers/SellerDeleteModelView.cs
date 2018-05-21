using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using App11.Models;
using App11.Services.ShareService;
using App11.Views.Sellers;
using Plugin.Settings;
using Xamarin.Forms;

namespace App11.ViewModels.Buyers.Sellers
{
    public class SellerDeleteModelView : INotifyPropertyChanged
    {

        public Command OpenOtherPageCommand { get; }
        INavigation Navigation;

        private readonly string _apiKey = (Application.Current as App).UserTokenKey;
        //  public ObservableCollection<CartProduct> usersCartList { get; set; }

        private List<ChatListModel> usersCartList;




        public List<ChatListModel> UsersCartList
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
        public Command<ChatListModel> RemoveComand

        {

            get

            {
                return new Command<ChatListModel>(async (CartProduct) =>

                {


                    string userApiKey = CrossSettings.Current.GetValueOrDefault("APIKey", "unknown");
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                        var values = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("api_key",_apiKey),
                        new KeyValuePair<string,string>("conversation_id",CartProduct.id)


                    });

                        var client = new HttpClient();
                        var response = await client.PostAsync("http://dev.foodforus.cloud/public/api/v1/hideConversation", values);
                        var respond = await response.Content.ReadAsStringAsync();
                        UserDialogs.Instance.HideLoading();


                        UserDialogs.Instance.ShowSuccess("Conversation was successfully Remove", 3000);

                        //    Debug.WriteLine("serveR:" + CartProduct.productId + "and " + CartProduct.id + "Productype" + CartProduct.productType + "apikey" + userApiKey);

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

        public SellerDeleteModelView()

        {
            InitializeDataAsync();
        }



        public SellerDeleteModelView(INavigation MainPageNav)

        {
            Navigation = MainPageNav;
            OpenOtherPageCommand = new Command(async () => await OpenOtherPage());

            InitializeDataAsync();



        }

        public async Task OpenOtherPage()
        {
            await Navigation.PushModalAsync(new SellersMaster());
        }



        private async Task InitializeDataAsync()
        {

            //  var userserve = new CaseServices();
            var CartProductDatas = new ChatListingService();

            UsersCartList = await CartProductDatas.GetChatListingdetails();


        }



        public event PropertyChangedEventHandler PropertyChanged;



        protected virtual void OnpropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
