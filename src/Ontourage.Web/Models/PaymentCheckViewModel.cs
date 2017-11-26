using Ontourage.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class PaymentCheckViewModel
    {
        [Display(Name = "№")]
        public int Id { get; set; }

        [Display(Name = "Имя клиента")]
        public ClientAggregateViewModel Client { get; set; }

        [Display(Name = "Название тура")]
        public VoucherAggregateViewModel Voucher { get; set; }

        [Display(Name = "Количество заказаных путевок")]
        public int CountOfVouchers { get; set; }

        [Display(Name = "Общая стоимость")]
        public double TotalPrice { get; set; }

        [Display(Name = "Дата продажи")]
        public DateTime DateOfSale { get; set; }

        public PaymentCheckViewModel(PaymentCheck paymentCheck)
        {
            BindFromModel(paymentCheck);
        }

        public void BindFromModel(PaymentCheck paymentCheck)
        {
            Id = paymentCheck.Id;
            Client = new ClientAggregateViewModel(paymentCheck.Client);
            Voucher = new VoucherAggregateViewModel(paymentCheck.Voucher);
            CountOfVouchers = paymentCheck.CountOfVouchers;
            TotalPrice = paymentCheck.TotalPrice;
            DateOfSale = paymentCheck.DateOfSale;
        }
        public PaymentCheck CreateFromViewModel()
        {
            return new PaymentCheck(Id, new ClientAggregate(Client.Id, Client.FirstName, Client.LastName, Client.Sex, Client.DateOfBirth,
                    Client.Passport, Client.PhoneNumber, Client.Email, new Discount(Client.Discount.Id, Client.Discount.Type, Client.Discount.Count),
                    Client.UserLevel),
                new VoucherAggregate(Voucher.Id, Voucher.TourName,
                    new HotelAggregate(Voucher.Hotel.Id, Voucher.Hotel.HotelName,
                        new Country(Voucher.Hotel.Country.CountryCode, Voucher.Hotel.Country.CountryCode), Voucher.Hotel.CountOfStars),
                    Voucher.PassageInclude,
                    new FoodType(Voucher.FoodType.Id, Voucher.FoodType.Name),
                    new TourOperator(Voucher.TourOperator.Id, Voucher.TourOperator.TourOperatorName),
                    Voucher.Price, Voucher.CountFreeVouchers, Voucher.DepartureTime, Voucher.DeparturePlace, Voucher.ArrivalTime, Voucher.ArrivalPlace),
                    CountOfVouchers, TotalPrice, DateOfSale);
        }
    }
}
