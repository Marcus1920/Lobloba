using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.Services.ShareService
{
    public  class UpdateIntrestServices
    {
        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;

        public async Task<bool> UpdateintresteAsync(string userIstresName)
        {

            _url = "http://system.foodforus.cloud/api/v1/changeDefaultLocation";
           


        var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("apiKey", _apiKey),
                new KeyValuePair<string, string>("userIntrest", userIstresName.ToString()),
                
            });

            _response = await _client.PostAsync(_url, form);

            return _response.IsSuccessStatusCode;

        }

    }
}
