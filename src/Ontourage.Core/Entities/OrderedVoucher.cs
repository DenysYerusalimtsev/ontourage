using System;
using System.Collections.Generic;
using System.Text;

namespace Ontourage.Core.Entities
{
    public class OrderedVoucher
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string TourName { get; set; }

        public string Country { get; set; }

        public string Hotel { get; set; }

        public bool PassageInclude { get; set; }

        public string FoodType { get; set; }

        public string TourOperator { get; set; }

        public int CountFreeVouchers { get; set; }

        public DateTime DepartureTime { get; set; }

        public string DeparturePlace { get; set; }

        public DateTime ArrivalTime { get; set; }

        public string ArrivalPlace { get; set; }

        public int CountOrderedVouchers { get; set; }

        public double TotalPrice { get; set; }

        public OrderedVoucher(
         int id,
         int clientId,
         string tourName,
         string country,
         string hotel,
         bool passageInclude,
         string foodType,
         string tourOperator,
         int countFreeVouchers,
         DateTime departureTime,
         string departurePlace,
         DateTime arrivalTime,
         string arrivalPlace,
         int countOrderedVouchers,
         double totalPrice)
        {
            Id = id;
            ClientId = clientId;
            TourName = tourName;
            Country = country;
            Hotel = hotel;
            PassageInclude = passageInclude;
            FoodType = foodType;
            TourOperator = tourOperator;
            CountFreeVouchers = countFreeVouchers;
            DepartureTime = departureTime;
            DeparturePlace = departurePlace;
            ArrivalTime = arrivalTime;
            ArrivalPlace = arrivalPlace;
            CountOrderedVouchers = countOrderedVouchers;
            TotalPrice = totalPrice;
        }
    }
}
