using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using App11;
using App11.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace App11.Models
{
    public class ChatModel 
    {
        public string name { get; set; }

        public string surname { get; set; }
        public string conversation_id { get; set; }
        
        public string message
        {
            get; set;
        }
        public DateTime created_at { get; set; }

        public string ImageUrl { get; set; }
        public string user_type { get; set; }

       

    }
}



public class Chatcallback
{
    #region HistoryList
    private readonly string _apiKey = (Application.Current as App).UserTokenKey;
    public async Task<List<ChatModel>> GetChatlistmessage(String PostId)
    {
    

        var values = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("post_id", PostId),
            new KeyValuePair<string, string>("api_key",_apiKey)

        });
    

         var client = new HttpClient();
        var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/conversation", values);
        var respond = await response.Content.ReadAsStringAsync();
        var message = JsonConvert.DeserializeObject<List<ChatModel>>(respond);
     
        return message;
    }
    #endregion
}


