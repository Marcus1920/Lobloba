using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace App11.Services.SellersServices
{
    public class LocationService
    {
        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;

        public async System.Threading.Tasks.Task<bool> SetPickUpPoint(Position position)
        {
            _url = "http://system.foodforus.cloud/api/v1/changeDefaultLocation";


            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("apiKey", _apiKey),
                new KeyValuePair<string, string>("gps_lat", position.Latitude.ToString()),
                new KeyValuePair<string, string>("gps_long", position.Longitude.ToString())
            });

            _response = await _client.PostAsync(_url, form);

            return _response.IsSuccessStatusCode;
        }
    }
}
