using System;

namespace Ontourage.Core.Entities
{
    public class VoucherAggregate
    {
        public int Id { get; set; }

        public string TourName { get; set; }

        public HotelAggregate Hotel { get; set; }

        public bool PassageInclude { get; set; }

        public FoodType FoodType { get; set; }

        public TourOperator TourOperator { get; set; }

        public double Price { get; set; }

        public int CountFreeVouchers { get; set; }

        public DateTime DepartureTime { get; set; }

        public string DeparturePlace { get; set; }

        public DateTime ArrivalTime { get; set; }

        public string ArrivalPlace { get; set; }

        public VoucherAggregate(
            int id,
            string tourName,                                                  
            HotelAggregate hotel,
            bool passageInclude,
            FoodType foodType,
            TourOperator tourOperator,
            double price,
            int countFreeVouchers,
            DateTime departureTime,
            string departurePlace,
            DateTime arrivalTime,
            string arrivalPlace)
        {
            Id = id;
            TourName = tourName;
            Hotel = hotel;
            PassageInclude = passageInclude;
            FoodType = foodType;
            TourOperator = tourOperator;
            Price = price;
            CountFreeVouchers = countFreeVouchers;
            DepartureTime = departureTime;
            DeparturePlace = departurePlace;
            ArrivalTime = arrivalTime;
            ArrivalPlace = arrivalPlace;
        }
    }
}
