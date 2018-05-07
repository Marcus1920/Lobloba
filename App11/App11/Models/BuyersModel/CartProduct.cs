using Newtonsoft.Json;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App11.Models.BuyersModel
{
    public class CartProduct
    {

        public string id { get; set; }
        public string userId { get; set; }
        public string new_user_id { get; set; }
        public string productId { get; set; }
        public string productType { get; set; }
        public string quantity { get; set; }
        public string name { get; set; }
        public string productName { get; set; }
        public string productPicture { get; set; }
        public string created_at { get; set; }

    }

    public class CartProductData
    {
        public async Task<List<CartProduct>> GetCartProducts()
        {
            string userApiKey = CrossSettings.Current.GetValueOrDefault("APIKey", "unknown");
            var client = new HttpClient();
            var contents = new MultipartContent();
            var json = await client.GetStringAsync("http://system.foodforus.cloud/api/v1/getCartItem?api_key=" + userApiKey);
            var poststList = JsonConvert.DeserializeObject<List<CartProduct>>(json);

            return poststList;
        }
    }
}
