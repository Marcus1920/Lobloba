using App11.Models.SellersModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.Services.SellersServices
{
    public class TransactionsService
    {
        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;


        public async Task<List<Transaction>> GetAllTransactions()
        {
            _url = "http://system.foodforus.cloud/api/v1/transactionDetails?api_key=" + _apiKey;

            var json = await _client.GetStringAsync(_url);

            var transactionList = JsonConvert.DeserializeObject<List<Transaction>>(json);

            return transactionList;
        }

        public async Task<bool> ChangeTransactionStatus(Transaction transaction)
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

        public async Task<bool> RateTransaction(Transaction transaction)
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
