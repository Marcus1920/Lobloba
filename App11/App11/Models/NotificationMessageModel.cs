using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App11.Helpers;
using Newtonsoft.Json;
using Plugin.Settings;
using Xamarin.Forms;

namespace App11.Models
{
    public  class NotificationMessageModel
    {
        public string id   { set; get; }
        public string new_user_id  { set; get; }
        public string PostId  { set; get; }
        public string ProductName { set; get; }
        public string Status { set; get; }
        public string Message { set; get; }
        public string productType { set; get; }
        
        public string productPicture { get; set; }
        public DateTime created_at { get; set; }

        public string location { get; set; }
        public string gps_lat { get; set; }
        public string productTypeId { get; set; }

        public string quantity { get; set; }
        public string costPerKg { get; set; }

        public string description { get; set; }
        public string country { get; set; }

        public string city { get; set; }

        public string packaging { get; set; }

        public string availableHours { get; set; }
        public string paymentMethods { get; set; }
        public string transactionRating { get; set; }

        public string updated_at { get; set; }

        public string sellByDate { get; set; }

        public string pickUpAddress { get; set; }


        public string monToFridayHours { get; set; }


        public string saturdayHours { get; set; }

        public string sundayHours { get; set; }

    }


    public class Callbackdata
    {
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;
        
        public async Task<List<NotificationMessageModel>> GetNotificationMessage()
        {
            
            var client = new HttpClient();
            var contents = new MultipartContent();
            var json = await client.GetStringAsync("http://system.foodforus.cloud/api/v1/notification?api_key=" + _apiKey);
            var poststList = JsonConvert.DeserializeObject<List<NotificationMessageModel>>(json);

            return poststList;
        }
    }


}
