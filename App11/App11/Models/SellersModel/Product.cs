using System;
using Plugin.Media.Abstractions;
using App11.ViewModels.Logic;

namespace App11.Models.SellersModel
{
    public class Product
    {
        public int Id { get; set; }
        public String ProductType { get; set; }
        public string Description { get; set; }
        public double TransactionRating { get; set; }
        public double CostPerKg { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ProductPicture { get; set; }
        public double Quantity { get; set; }
        public string Packaging { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AvailableHours { get; set; }
        public string PaymentMethods { get; set; }
        public string MonToFridayHours { get; set; }
        public string SaturdayHours { get; set; }
        public string SundayHours { get; set; }
        public string PickUpAddress { get; set; }
        public DateTime SellByDate { get; set; }

        public string Summary => (new AppFunctions()).Summerize(Description);
        public MediaFile File { get; set; }
        public bool Donation => CostPerKg == 0;
        public bool NotDonation => !Donation;
        public string TimeFrom => AvailableHours.Split('-')[0];
        public string TimeTo => AvailableHours.Split('-')[1];
        public string AvailabilityTimes => TimeFrom + " to " + TimeTo;

        public string WeekFrom => MonToFridayHours.Split('-')[0];
        public string WeekTo => MonToFridayHours.Split('-')[1];
        public string WeekTimes => WeekFrom + " to " + WeekTo;

        public string SatFrom => SaturdayHours.Split('-')[0];
        public string SatTo => SaturdayHours.Split('-')[1];
        public string SatTimes => SatFrom + " to " + SatTo;

        public string SunFrom => SundayHours.Split('-')[0];
        public string SunTo => SundayHours.Split('-')[1];
        public string SunTimes => SunFrom + " to " + SunTo;
    }
}

