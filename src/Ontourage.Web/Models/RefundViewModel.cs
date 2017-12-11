using System;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class RefundViewModel
    {
        public int Id { get; set; }

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
    }
}