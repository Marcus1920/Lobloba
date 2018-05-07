using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using App11.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace App11.ViewModels
{
   public class ChatViewmodel
    {

        private List<ChatModel> _chats;
        Chatcallback getChatcallback = new Chatcallback();
        private String  outGoingText ="";

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
            try
            {

                var values = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("conversation_id", "10"),
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
                OutGoingText  = String.Empty;
                Debug.WriteLine(respond + "?ioooooooooooooooooooooooooooooooooooooooooo");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hello", ex);
            }
        }
    }
}
