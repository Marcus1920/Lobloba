using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace App11.Models
{
    public class AlertproductModel
    {

        public string id { set; get; }
        public string new_user_id { set; get; }
        public string productName { set; get; }
        public string active { set; get; }

      

        public  string StatusName => StatusRequest();
        private  String StatusRequest()
        {
             string  StatusNames = "" ;
             
            if (active =="1")
            {

                StatusNames = "Active";

            }
            else
            {
                StatusNames = "Inactive";
            }
            return StatusNames; 
        }

     

        public Color StatusColor => GetCour();

        private Color GetCour()
        {
            var color = Color.CadetBlue;

            switch (StatusName)
            {
                case "Active":
                    color = Color.Green;
                    break;

            

                case "Inactive":
                    color = Color.Tomato;
                    break;

                
            }

            return color;
        }
    }



    public class CallbackdataAlert
    {
        private readonly string _apiKey = (Application.Current as App).UserTokenKey;

        public async Task<List<AlertproductModel>> GetalertIntrestList()
        {

            var client = new HttpClient();
            var contents = new MultipartContent();
            var json = await client.GetStringAsync("http://system.foodforus.cloud/api/v1/getProductInterest?api_key=" + _apiKey);
            var poststList = JsonConvert.DeserializeObject<List<AlertproductModel>>(json);

            return poststList;
        }
    }

}
