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
        public string Country { get; set; }

        [Display(Name = "Название отеля")]
        public string Hotel { get; set; }

        [Display(Name = "Трансфер")]
        public bool PassageInclude { get; set; }

        [Display(Name = "Тип питания")]
        public string FoodType { get; set; }

        [Display(Name = "Туроператор")]
        public string TourOperator { get; set; }

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

        public void BindFromModel(Voucher voucher)
        {
            Id = voucher.Id;
            TourName = voucher.TourName;
            PassageInclude = voucher.PassageInclude;
            Price = voucher.Price;
            CountFreeVouchers = voucher.CountFreeVouchers;
            DepartureTime = voucher.DepartureTime;
            DeparturePlace = voucher.DeparturePlace;
            ArrivalTime = voucher.ArrivalTime;
            ArrivalPlace = voucher.ArrivalPlace;
        }
        public VoucherAggregate CreateFromViewModel()
        {
            return new VoucherAggregate(Id, TourName, Country, Hotel, PassageInclude,
                FoodType, TourOperator, Price, CountFreeVouchers, DepartureTime,
                DeparturePlace, ArrivalTime, ArrivalPlace);
        }
    }
}
