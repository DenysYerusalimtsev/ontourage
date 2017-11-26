using Ontourage.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class VoucherAggregateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя тура")]
        public string TourName { get; set; }

        [Display(Name = "Страна")]
        public CountryViewModel Country { get; set; }

        [Display(Name = "Название отеля")]
        public HotelAggregateViewModel Hotel { get; set; }

        [Display(Name = "Трансфер")]
        public bool PassageInclude { get; set; }

        [Display(Name = "Тип питания")]
        public FoodType FoodType { get; set; }

        [Display(Name = "Туроператор")]
        public TourOperatorViewModel TourOperator { get; set; }

        [Display(Name = "Цена за одну путевку")]
        public double Price { get; set; }

        [Display(Name = "Количество свободных мест")]
        public int CountFreeVouchers { get; set; }

        [Display(Name = "Время отбытия")]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Место отбытия")]
        public string DeparturePlace { get; set; }

        [Display(Name = "Время прибытия")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Место прибытия")]
        public string ArrivalPlace { get; set; }

        public int CountOrderVouchers { get; set; }

        public HeaderViewModel Header { get; set; }

        public VoucherAggregateViewModel(VoucherAggregate voucher)
        {
            BindFromModel(voucher);
        }

        public void BindFromModel(VoucherAggregate voucher)
        {
            Id = voucher.Id;
            TourName = voucher.TourName;
            Country = new CountryViewModel(voucher.Hotel.Country);
            Hotel = new HotelAggregateViewModel(voucher.Hotel);
            PassageInclude = voucher.PassageInclude;
            FoodType = new FoodType(voucher.FoodType.Id, voucher.FoodType.Name);
            TourOperator = new TourOperatorViewModel(voucher.TourOperator.Id, 
                voucher.TourOperator.TourOperatorName);
            Price = voucher.Price;
            CountFreeVouchers = voucher.CountFreeVouchers;
            DepartureTime = voucher.DepartureTime;
            DeparturePlace = voucher.DeparturePlace;
            ArrivalTime = voucher.ArrivalTime;
            ArrivalPlace = voucher.ArrivalPlace;
        }

        public VoucherAggregate CreateFromViewModel()
        {
            return new VoucherAggregate(Id, TourName, new HotelAggregate(Hotel.Id, Hotel.HotelName,
                    new Country(Country.CountryCode, Country.CountryCode), Hotel.CountOfStars), PassageInclude, new FoodType(FoodType.Id, 
                FoodType.Name), new TourOperator(TourOperator.Id, TourOperator.TourOperatorName), 
                Price, CountFreeVouchers, DepartureTime, DeparturePlace, ArrivalTime, ArrivalPlace);
        }
    }
}