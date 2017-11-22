using System;

namespace Ontourage.Core.Entities
{
    public class Voucher
    {
        public int Id { get; set; }

        public string TourName { get; set; }

        public string CountryCode { get; set; }

        public int HotelId { get; set; }

        public bool PassageInclude { get; set; }

        public int FoodTypeId { get; set; }

        public int TourOperatorId { get; set; }

        public double Price { get; set; }

        public int CountFreeVouchers { get; set; }

        public DateTime DepartureTime { get; set; }

        public string DeparturePlace { get; set; }

        public DateTime ArrivalTime { get; set; }

        public string ArrivalPlace { get; set; }

        public Voucher(
            int id, 
            string tourName, 
            string countryCode, 
            int hotelId, 
            bool passageInclude, 
            int foodTypeId, 
            int tourOperatorId, 
            double price,
            int countFreeVouchers, 
            DateTime departureTime, 
            string departurePlace,
            DateTime arrivalTime, 
            string arrivalPlace)
        {
            Id = id;
            TourName = tourName;
            CountryCode = countryCode;
            HotelId = hotelId;
            PassageInclude = passageInclude;
            FoodTypeId = foodTypeId;
            TourOperatorId = tourOperatorId;
            Price = price;
            CountFreeVouchers = countFreeVouchers;
            DepartureTime = departureTime;
            DeparturePlace = departurePlace;
            ArrivalTime = arrivalTime;
            ArrivalPlace = arrivalPlace;
        }
    }
}
