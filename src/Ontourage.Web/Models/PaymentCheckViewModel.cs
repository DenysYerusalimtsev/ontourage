using Ontourage.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class PaymentCheckViewModel
    {
        [Display(Name = "№")]
        public int Id { get; set; }

        int ClientId { get; set; }

        [Display(Name = "Имя клиента")]
        public string ClientFirstName { get; set; }

        [Display(Name = "Фамилия клиента")]
        public string ClientLastName { get; set; }

        [Display(Name = "Паспортные данные")]
        public string ClientPassport { get; set; }

        public int VoucherId { get; set; }

        [Display(Name = "Название тура")]
        public string VoucherName { get; set; }

        [Display(Name = "Количество заказаных путевок")]
        public int CountOfVouchers { get; set; }

        [Display(Name = "Общая стоимость")]
        public double TotalPrice { get; set; }

        [Display(Name = "Дата продажи")]
        public DateTime DateOfSale { get; set; }

        public void BindFromModel(PaymentCheck paymentCheck)
        {
            Id = paymentCheck.Id;
            ClientId = paymentCheck.ClientId;
            VoucherId = paymentCheck.VoucherId;
            CountOfVouchers = paymentCheck.CountOfVouchers;
            TotalPrice = paymentCheck.TotalPrice;
            DateOfSale = paymentCheck.DateOfSale;
    }
        public PaymentCheck CreateFromViewModel()
        {
            return new PaymentCheck(Id, ClientId, VoucherId, CountOfVouchers, TotalPrice, DateOfSale);
        }
    }
}
