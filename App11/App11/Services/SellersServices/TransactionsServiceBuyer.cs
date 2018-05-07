using App11.Models.BuyersModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.Services.SellersServices
{
    public class TransactionsServiceBuyer
    {

        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;


        public async Task<List<HistoryTransaction>> GetAllTransactions()
        {



            _url = "http://system.foodforus.cloud/api/v1/transactionDetails?api_key=" + _apiKey;

            var json = await _client.GetStringAsync(_url);
            var transactionList = JsonConvert.DeserializeObject<List<HistoryTransaction>>(json);

            return transactionList;
        }

        public async Task<bool> ChangeTransactionStatus(HistoryTransaction transaction)
        {
            _url = "http://system.foodforus.cloud/api/v1/approveTransaction";

            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("api_key", _apiKey),
                new KeyValuePair<string, string>("transactionId", transaction.TransactionId.ToString()),
                new KeyValuePair<string, string>("statusName", transaction.TransactionName)
            });

            _response = await _client.PostAsync(_url, form);


            return _response.IsSuccessStatusCode;
        }

        public async Task<bool> RateTransaction(HistoryTransaction transaction)
        {
            _url = "http://system.foodforus.cloud/api/v1/transactionRating";

            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("apiKey", _apiKey),
                new KeyValuePair<string, string>("transactionId", transaction.TransactionId.ToString()),
                new KeyValuePair<string, string>("rating", transaction.Rating),
                new KeyValuePair<string, string>("comment", transaction.Comment)
            });

            _response = await _client.PostAsync(_url, form);


            return _response.IsSuccessStatusCode;
        }
    }
}
