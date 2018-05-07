using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace App11.Models.BuyersModel
{
    public class Product
    {
        public string id { get; set; }
        public string productType { get; set; }
        public string Description { get; set; }
        public string productTypeId { get; set; }
        public double transactionRating { get; set; }
        public string country { get; set; }
        public string City { get; set; }
        public string quantity { get; set; }
        public bool isFavoutite { get; set; }
        public string costPerKg { get; set; }
        public string productPicture { get; set; }
        public double Quantity { get; set; }
        public string Packaging { get; set; }

        public string gps_lat { get; set; }
        public string gps_long { get; set; }
        public string availableHours { get; set; }
        public string PaymentMethods { get; set; }
        public string monToFridayHours { get; set; }
        public string saturdayHours { get; set; }
        public string SundayHours { get; set; }
        public string PickUpAddress { get; set; }

        public DateTime SellByDate { get; set; }


        public DateTime created_at { get; set; }

    }

    public class Data
    {
        public async Task<List<Product>> GetProducts()
        {
            //var isConnected = CrossConnectivity.Current.IsConnected;


            var client = new HttpClient();
            var contents = new MultipartContent();
            var json = await client.GetStringAsync("http://system.foodforus.cloud/api/v1/allSellersPost");
            var poststList = JsonConvert.DeserializeObject<List<Product>>(json);

            return poststList;
        }
    }
}

