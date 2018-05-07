using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Plugin.Settings;
using System.Net.Http;
using Newtonsoft.Json;
using App11.Models.BuyersModel;
using Xamarin.Forms;
using App11.Models.BuyersModel;

namespace App11.Models.BuyersModel
{
    public class HistoryTransaction
    {

        public int TransactionId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string ProfilePicture { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string Location { get; set; }
        public string DescriptionOfAcces { get; set; }
        public double Quantity { get; set; }
        public string TransactionName { get; set; }
        public string ProductName { get; set; }
        public string ProductPicture { get; set; }
        public DateTime Created_at { get; set; }
        public string Rating { get; set; }
        public string Comment { get; set; }

        public string FullNames => Name + " " + SurName;
        public string Description => Quantity
            + " kg of "
            + ProductName
            + " requested..";

        public void AcceptOrder()
        {
            TransactionName = "Active";
        }

        public void DeclineOrder()
        {
            TransactionName = "Declined";
        }

        public void CloseTransaction()
        {
            TransactionName = "Completed";
        }

        public Color StatusColor => GetCour();

        private Color GetCour()
        {
            var color = Color.CadetBlue;

            switch (TransactionName)
            {
                case "New":
                    color = Color.Blue;
                    break;

                case "Active":
                    color = Color.Green;
                    break;

                case "Cancelled":
                    color = Color.OrangeRed;
                    break;

                case "Declined":
                    color = Color.Red;
                    break;
            }

            return color;
        }
    }
}


public class HistoryData
{
    #region HistoryList
    public async Task<List<HistoryTransaction>> GetHistoryList()
    {
        string userApiKey = CrossSettings.Current.GetValueOrDefault("APIKey", "unknown");
        var client = new HttpClient();
        var contents = new MultipartContent();
        var json = await client.GetStringAsync("http://154.0.164.72:8080/Foods/api/v1/transactionDetails?api_key=" + userApiKey);
        var poststList = JsonConvert.DeserializeObject<List<HistoryTransaction>>(json);

        return poststList;
    }
    #endregion
}

