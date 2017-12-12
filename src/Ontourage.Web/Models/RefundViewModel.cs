using System;
using System.ComponentModel.DataAnnotations;
using Ontourage.Core.Entities;

namespace Ontourage.Web.Models
{
    public class RefundViewModel
    {
        public int Id { get; set; }

        public int PaymentCheckId { get; set; }

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

        public void BindFromModel(PaymentCheck check)
        {
            PaymentCheckId = check.Id;
            Client = new ClientAggregateViewModel(check.Client);
            Voucher = new VoucherAggregateViewModel(check.Voucher);
            TotalPrice = check.TotalPrice;
            DateOfSale = check.DateOfSale;
            DateOfReturn = DateTime.Now;
        }

        public Refund CreateFromViewModel()
        {
            return new Refund(Id, PaymentCheckId, DateTime.Now);
        }
    }
}