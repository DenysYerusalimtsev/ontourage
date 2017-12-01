using Ontourage.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class VoucherViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя тура")]
        [Required(ErrorMessage = "Имя тура является обязательным полем")]
        public string TourName { get; set; }

        [Display(Name = "Код страны")]
        public string CountryCode { get; set; }
        public IEnumerable<Country> Countries { get; set; }

        [Display(Name = "Название отеля")]
        public int HotelId { get; set; }

        public IEnumerable<HotelAggregate> Hotels { get; set; }

        [Display(Name = "Трансфер")]
        public bool PassageInclude { get; set; }

        [Display(Name = "Тип питания")]
        public int FoodTypeId { get; set; }
        public IEnumerable<FoodType> FoodTypes { get; set; }

        [Display(Name = "Туроператор")]
        public int TourOperatorId { get; set; }

        public IEnumerable<TourOperator> TourOperators { get; set; }

        [Display(Name = "Цена за одну путевку")]
        [Required(ErrorMessage = "Цена за одну путевку является обязательным полем")]
        public double Price { get; set; }

        [Display(Name = "Количество свободных мест")]
        [Required(ErrorMessage = "Количество свободных мест является обязательным полем")]
        public int CountFreeVouchers { get; set; }

        [Display(Name = "Время отбытия")]
        [Required(ErrorMessage = "Время отбытия является обязательным полем")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Место отбытия")]
        [Required(ErrorMessage = "Место отбытия является обязательным полем")]
        public string DeparturePlace { get; set; }

        [Display(Name = "Время прибытия")]
        [Required(ErrorMessage = "Время прибытия является обязательным полем")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Место прибытия")]
        [Required(ErrorMessage = "Место прибытия является обязательным полем")]
        public string ArrivalPlace { get; set; }

        public HeaderViewModel Header { get; set; }

        public void BindFromModel(VoucherAggregate voucher)
        {
            Id = voucher.Id;
            TourName = voucher.TourName;
            CountryCode = voucher.Hotel.Country.CountryCode;
            HotelId = voucher.Hotel.Id;
            PassageInclude = voucher.PassageInclude;
            FoodTypeId = voucher.FoodType.Id;
            TourOperatorId = voucher.TourOperator.Id;
            Price = voucher.Price;
            CountFreeVouchers = voucher.CountFreeVouchers;
            DepartureTime = voucher.DepartureTime;
            DeparturePlace = voucher.DeparturePlace;
            ArrivalTime = voucher.ArrivalTime;
            ArrivalPlace = voucher.ArrivalPlace;
        }

        public Voucher CreateFromViewModel()
        {
            return new Voucher( Id, TourName, CountryCode, HotelId, PassageInclude, 
                FoodTypeId,TourOperatorId, Price, CountFreeVouchers, DepartureTime,
                DeparturePlace, ArrivalTime, ArrivalPlace);
        }
    }
}
