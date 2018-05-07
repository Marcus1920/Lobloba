using Acr.UserDialogs;
using App11.Models;
using App11.ViewModels;
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
	public partial class Chat : ContentPage
	{

        public readonly string PostId;
        public readonly string ConversationID;
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;
        ChatViewmodel _chatViewmodel;
        ChatListModel Chatdetails = new ChatListModel();
        private ObservableCollection<ChatModel> _transactions;
        public int size, newsize;
        Chatcallback data = new Chatcallback();
        public Chat(ChatListModel Chatdetails)
        {


            InitializeComponent();
            BindingContext = Chatdetails;
            this.Chatdetails = Chatdetails;

            PostId = this.Chatdetails.Post_id;
            ConversationID = this.Chatdetails.id;

            /*_chatViewmodel = new ChatViewmodel();
            BindingContext = this._chatViewmodel;*/
        }
        protected override async void OnAppearing()
        {
            var allProducts = await data.GetChatlistmessage(PostId);
            _transactions = new ObservableCollection<ChatModel>(allProducts);
            Listmesage.ItemsSource = _transactions;


            base.OnAppearing();
        }

        private async void Listmesage_OnRefreshingAsync(object sender, EventArgs e)
        {
            var allProducts = await data.GetChatlistmessage(PostId);
            _transactions = new ObservableCollection<ChatModel>(allProducts);
            Listmesage.ItemsSource = _transactions;

            Listmesage.EndRefresh();




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


            var client = new HttpClient();
            var response = await client.PostAsync("http://dev.foodforus.cloud/public/api/v1/conversation", values);
            var respond = await response.Content.ReadAsStringAsync();
            size = respond.Length;

            StartTimer();
        }





        async Task getSizeAsync()
        {
            var values = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("post_id", PostId),
                new KeyValuePair<string, string>("api_key", _apiKey)

            });
            var client = new HttpClient();
            var response = await client.PostAsync("http://dev.foodforus.cloud/public/api/v1/conversation", values);
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

        private async void Button_OnClickedAsync(object sender, EventArgs e)
        {




            try
            {

                var values = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("conversation_id", ConversationID),
                    new KeyValuePair<string, string>("api_key", _apiKey),
                    new KeyValuePair<string, string>("message", Outgo.Text)

                });

                var client = new HttpClient();

                var response = await client.PostAsync("http://dev.foodforus.cloud/public/api/v1/createMessage", values);
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