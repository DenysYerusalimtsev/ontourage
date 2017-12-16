using System;
using System.ComponentModel.DataAnnotations;
using Ontourage.Core.Entities;

namespace Ontourage.Web.Models
{
    public class RefundViewModel
    {
        public int Id { get; set; }

        public PaymentCheck Check { get; set; }

        [Display(Name = "Имя клиента")]
        public ClientAggregateViewModel Client { get; set; }

        [Display(Name = "Название тура")]
        public VoucherAggregateViewModel Voucher { get; set; }

        [Display(Name = "Общая стоимость")]
        public double TotalPrice { get; set; }

        [Display(Name = "Дата продажи")]
        public DateTime DateOfSale { get; set; }

        [Display(Name = "Дата возврата")]
        public DateTime DateOfReturn { get; set; }

        public void BindFromModel(Refund refund)
        {
            Id = refund.Id;
            Check = refund.PaymentCheck;
            Client = new ClientAggregateViewModel(refund.PaymentCheck.Client);
            Voucher = new VoucherAggregateViewModel(refund.PaymentCheck.Voucher);
            TotalPrice = refund.PaymentCheck.TotalPrice;
            DateOfSale = refund.PaymentCheck.DateOfSale;
            DateOfReturn = DateTime.Now;
        }

        public Refund CreateFromViewModel()
        {
            return new Refund(Id, Check, DateTime.Now);
        }
    }
}