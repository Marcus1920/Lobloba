using App11.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.Services.ShareService
{
    class ChatListingService
    {


        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;



        public async Task<List<ChatListModel>> GetChatListingdetails()
        {

            _url = "http://dev.foodforus.cloud/public/api/v1/getMyConversation?api_key=" + _apiKey;

            var json = await _client.GetStringAsync(_url);
            var transactionList = JsonConvert.DeserializeObject<List<ChatListModel>>(json);

            return transactionList;
        }


    }
}
