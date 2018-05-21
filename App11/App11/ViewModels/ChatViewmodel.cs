using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using App11.Models;
using App11.Views.Buyers;
using Newtonsoft.Json;
using Plugin.Settings;
using Xamarin.Forms;

namespace App11.ViewModels
{
    public class ChatViewmodel : INotifyPropertyChanged
    {

        private List<ChatModel> _chats;
        Chatcallback getChatcallback = new Chatcallback();
        private String outGoingText = "";
        public Command OpenOtherPageCommand { get; }
        INavigation Navigation;


        public List<ChatModel> chats { get; set; }
        public Command SendCommand { get; }
        public String OutGoingText
        {

            get { return outGoingText; }

            set
            {

                outGoingText = value;


            }

        }

        public ChatViewmodel(INavigation MainPageNav)
        {
            Navigation = MainPageNav;
            OpenOtherPageCommand = new Command(async () => await OpenOtherPage());

            InitialiazeChatAsync();

            SendCommand = new Command(Send);
        }

        public ChatViewmodel()
        {

            InitialiazeChatAsync();

            SendCommand = new Command(Send);
        }

        private void InitialiazeChatAsync()
        {

        }


        private async void Send()
        {
            /*  try
            {

                 var values = new FormUrlEncodedContent(new[]
                   {
                       new KeyValuePair<string, string>("conversation_id" ,"1" ),
                       new KeyValuePair<string, string>("api_key", "59423"),
                       new KeyValuePair<string, string>("message", outGoingText)

                   });

                   var client = new HttpClient();

                   var response = await client.PostAsync("http://dev.foodforus.cloud/public/api/v1/createMessage", values);
                   var respond = await response.Content.ReadAsStringAsync();

                   var toastConfig = new ToastConfig("Message Sent");
                   toastConfig.SetDuration(3000);
                   toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));

                   UserDialogs.Instance.Toast(toastConfig);
                   OutGoingText = String.Empty;
                   Debug.WriteLine(respond + "?ioooooooooooooooooooooooooooooooooooooooooo");

               }
               catch (Exception ex)
               {
                   Debug.WriteLine("Hello", ex);
               }
                 */
        }

        public async Task OpenOtherPage()
        {
            await Navigation.PushModalAsync(new BuyersHome());
        }

        private List<ChatModel> chalistdelet;




        public List<ChatModel> Chalistdelet
        {
            get
            {
                return chalistdelet;
            }

            set
            {
                chalistdelet = value;
                OnPropertyChanged();

            }

        }


        public Command<ChatModel> RemoveComand

        {

            get

            {
                return new Command<ChatModel>(async (Chalistdelet) =>

                {

                    //UsersCartList.Remove(CartProduct);
                    string userApiKey = CrossSettings.Current.GetValueOrDefault("APIKey", "unknown");
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                        var values = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("api_key",userApiKey),
                        new KeyValuePair<string,string>("conversation_id",Chalistdelet.conversation_id),


                    });

                        var client = new HttpClient();
                        var response = await client.PostAsync("http://dev.foodforus.cloud/public/api/v1/hideConversation", values);
                        var respond = await response.Content.ReadAsStringAsync();
                        UserDialogs.Instance.HideLoading();


                        UserDialogs.Instance.ShowSuccess("Product was successfully Remove", 3000);

                        //   Debug.WriteLine("serveR:" + Chalistdelet.conversation_id + "and " + Chalistdelet.id + "Productype" + CartProduct.productType + "apikey" + userApiKey);

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


        public event PropertyChangedEventHandler PropertyChanged;

    
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
