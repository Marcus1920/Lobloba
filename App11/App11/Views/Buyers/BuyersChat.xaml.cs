using Acr.UserDialogs;
using App11.Models;
using App11.Models.BuyersModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Buyers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuyersChat : ContentPage
	{
        public static string ChatId;
        public readonly string PostId;
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;
        Product product = new Product();
        private ObservableCollection<ChatModel> _transactions;
        Chatcallback data = new Chatcallback();

        public int size, newsize;
        public BuyersChat(Product product)
        {
            BindingContext = product;
            this.product = product;
            InitializeComponent();
            PostId = this.product.id;

            ChatInit();
        }

        private async Task ChatInit()
        {
            var valuesS = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("post_id", PostId),
                new KeyValuePair<string, string>("api_key",_apiKey)

            });
            var clients = new HttpClient();
            var contents = new MultipartContent();
            var response = await clients.PostAsync("http://system.foodforus.cloud/api/v1/getConversation", valuesS);
            var respond = await response.Content.ReadAsStringAsync();



            LastChatModel respon = JsonConvert.DeserializeObject<LastChatModel>(respond);

            ChatId = respon.conversation_id;

          //    await DisplayAlert("Alert", ChatId, "Ok");


        }

        protected override async void OnAppearing()
        {
            var allProducts = await data.GetChatlistmessage(PostId);
            _transactions = new ObservableCollection<ChatModel>(allProducts);
            Listmesage.ItemsSource = _transactions;


            var lastMessage = this.Listmesage.ItemsSource.Cast<object>().LastOrDefault();
            if (null != lastMessage)
            {
                this.Listmesage.ScrollTo(lastMessage, ScrollToPosition.End, true);
            }

            base.OnAppearing();

            var values = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("post_id", PostId),
                new KeyValuePair<string, string>("api_key", _apiKey)

            });

            await ChatInit();
            var client = new HttpClient();
            var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/conversation", values);
            var respond = await response.Content.ReadAsStringAsync();
            size = respond.Length;

         //  StartTimer();
        }


        async Task getSizeAsync()
        {
            var values = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("post_id", PostId),
                new KeyValuePair<string, string>("api_key", _apiKey)

            });
            var client = new HttpClient();
            var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/conversation", values);
            var respond = await response.Content.ReadAsStringAsync();
            newsize = respond.Length;
        }

        private void StartTimer()
        {



            Device.StartTimer(System.TimeSpan.FromSeconds(1), () =>
            {
                getSizeAsync();

                if (size < newsize)
                {
                    OnAppearing();
                }

                // we must put if statement to check on database that there is new message or not 
                // if there is will run this function  OnAppearing()  to get all new message and update it to listview 
                //  if (checkmassgesAsync().Equals(""))
                //   {
                //   OnAppearing();
                //  }


                return true;

            });
        }


        private async void Listmesage_OnRefreshingnRefreshingAsync(object sender, EventArgs e)
        {
            var allProducts = await data.GetChatlistmessage(PostId);
            _transactions = new ObservableCollection<ChatModel>(allProducts);

            Listmesage.ItemsSource = _transactions;

            Listmesage.EndRefresh();
        }



        private async void Button_OnClickedckedAsync(object sender, EventArgs e)
        {

            try
            {


                var values = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("conversation_id",ChatId),
                    new KeyValuePair<string, string>("api_key", _apiKey),
                    new KeyValuePair<string, string>("message", Outgo.Text)

                });

                var client = new HttpClient();

                var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/createMessage", values);
                var respond = await response.Content.ReadAsStringAsync();

                var toastConfig = new ToastConfig("Message Sent");
                toastConfig.SetDuration(3000);
                toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));

                UserDialogs.Instance.Toast(toastConfig);
                Outgo.Text = String.Empty;

                var allProducts = await data.GetChatlistmessage(PostId);
                _transactions = new ObservableCollection<ChatModel>(allProducts);
                Listmesage.ItemsSource = _transactions;
                Debug.WriteLine(respond + "?ioooooooooooooooooooooooooooooooooooooooooo");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hello", ex);
            }
        }
    }
}