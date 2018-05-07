using App11.Models.SellersModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.Services.SellersServices
{
    public class SellersPostsService
    {
        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;


        public async Task<List<Product>> GetAllPosts()
        {
            _url = "http://system.foodforus.cloud/api/v1/all?api_key=" + _apiKey;

            var json = await _client.GetStringAsync(_url);
            var poststList = JsonConvert.DeserializeObject<List<Product>>(json);

            foreach (var post in poststList)
            {
                if (post.ProductPicture == null)
                    post.ProductPicture = "image_not_found.jpg";
            }

            return poststList;
        }

        public async Task<bool> AddNewPost(Product product)
        {
            _url = "http://system.foodforus.cloud/api/v1/created?api_key=" + _apiKey;

            var content = new MultipartFormDataContent();

            var values = new[]
            {
                new KeyValuePair<string, string>("productName", product.ProductType),
                new KeyValuePair<string, string>("costPerKg", product.CostPerKg.ToString()),
                new KeyValuePair<string, string>("transactionRating", product.TransactionRating.ToString()),
                new KeyValuePair<string, string>("description", product.Description),
                new KeyValuePair<string, string>("country", product.Country),
                new KeyValuePair<string, string>("city", product.City),
                new KeyValuePair<string, string>("quantity", product.Quantity.ToString()),
                new KeyValuePair<string, string>("packaging", product.Packaging),
                new KeyValuePair<string, string>("gps_lat", product.Latitude),
                new KeyValuePair<string, string>("gps_long", product.Longitude),
                new KeyValuePair<string, string>("availableHours", product.AvailableHours),
                new KeyValuePair<string, string>("paymentMethods", product.PaymentMethods),
                new KeyValuePair<string, string>("MonToFridayHours", product.MonToFridayHours),
                new KeyValuePair<string, string>("SaturdayHours", product.SaturdayHours),
                new KeyValuePair<string, string>("SundayHours", product.SundayHours),
                new KeyValuePair<string, string>("sellByDate", product.SellByDate.ToString("yyyy-MM-dd H:mm:ss")),
                new KeyValuePair<string, string>("PickUpAddress", product.PickUpAddress)
            };

            foreach (var keyValuePair in values)
            {
                content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
            }

            if (product.File != null)
                content.Add(new StreamContent(product.File.GetStream()), "\"file\"", $"\"{product.ProductPicture}\"");

            _response = await _client.PostAsync(_url, content);

            return _response.IsSuccessStatusCode;
        }

        public async Task<bool> EditPost(Product product)
        {
            _url = "http://system.foodforus.cloud/api/v1/created?api_key=" + _apiKey;

            var content = new MultipartFormDataContent();

            var values = new[]
            {
                new KeyValuePair<string, string>("productName", product.ProductType),
                new KeyValuePair<string, string>("costPerKg", product.CostPerKg.ToString()),
                new KeyValuePair<string, string>("transactionRating", product.TransactionRating.ToString()),
                new KeyValuePair<string, string>("description", product.Description),
                new KeyValuePair<string, string>("country", product.Country),
                new KeyValuePair<string, string>("city", product.City),
                new KeyValuePair<string, string>("quantity", product.Quantity.ToString()),
                new KeyValuePair<string, string>("packaging", product.Packaging),
                new KeyValuePair<string, string>("gps_lat", product.Latitude),
                new KeyValuePair<string, string>("gps_long", product.Longitude)
            };

            foreach (var keyValuePair in values)
            {
                content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
            }

            if (product.File != null)
                content.Add(new StreamContent(product.File.GetStream()), "\"file\"", $"\"{product.ProductPicture}\"");

            _response = await _client.PostAsync(_url, content);

            return _response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePost(Product product)
        {
            _url = "http://system.foodforus.cloud/api/v1/deletePost";

            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("api_key", _apiKey),
                new KeyValuePair<string, string>("id", product.Id.ToString())
            });

            _response = await _client.PostAsync(_url, form);

            return _response.IsSuccessStatusCode;
        }

        public async Task<List<Product>> GetAllDonations()
        {
            _url = "http://system.foodforus.cloud/api/v1/all?api_key=" + _apiKey;

            var json = await _client.GetStringAsync(_url);
            var poststList = JsonConvert.DeserializeObject<List<Product>>(json);

            poststList.RemoveAll(p => p.NotDonation);

            foreach (var post in poststList)
            {
                if (post.ProductPicture == null)
                    post.ProductPicture = "image_not_found.jpg";
            }

            return poststList;
        }
    }
}
