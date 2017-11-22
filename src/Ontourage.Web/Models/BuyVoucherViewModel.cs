using Ontourage.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ontourage.Web.Models
{
    public class BuyVoucherViewModel
    {
        public int Id { get; set; }

        public int VoucherId { get; set; }

        public IEnumerable<Client> Clients { get; set; }

        [Display(Name = "Имя клиента")]
        [Required(ErrorMessage = "Имя клиента является обязательным полем")]
        public int ClientId { get; set; }

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

        [Display(Name = "Количество заказанных ваучеров")]
        [Required(ErrorMessage = "Количество заказанных ваучеров является обязательным полем")]
        public int CountOrderedVouchers { get; set; }

        [Display(Name = "Общая стоимость")]
        public double TotalPrice { get; set; }

        public void BindFromModel(Voucher voucher)
        {
            VoucherId = voucher.Id;
            TourName = voucher.TourName;
            PassageInclude = voucher.PassageInclude;
            Price = voucher.Price;
            DepartureTime = voucher.DepartureTime;
            DeparturePlace = voucher.DeparturePlace;
            ArrivalTime = voucher.ArrivalTime;
            ArrivalPlace = voucher.ArrivalPlace;
        }

        public PaymentCheck CreateFromViewModel()
        {
            return new PaymentCheck(Id, ClientId, VoucherId, CountOrderedVouchers, 
                TotalPrice = CountOrderedVouchers * Price, DateTime.Now);
        }
    }
}