using System;
using Xamarin.Forms;

namespace App11.Models.SellersModel
{
    public class Transaction
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
