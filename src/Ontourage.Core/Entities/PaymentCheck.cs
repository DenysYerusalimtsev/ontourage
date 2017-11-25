using System;

namespace Ontourage.Core.Entities
{
    public class PaymentCheck
    {
        public int Id { get; set; }

        public ClientAggregate Client { get; set; }

        public VoucherAggregate Voucher { get; set; }

        public int CountOfVouchers { get; set; }

        public double TotalPrice { get; set; }

        public DateTime DateOfSale { get; set; }

        public PaymentCheck(int id, ClientAggregate client, VoucherAggregate voucher, int countOfVouchers,
            double totalPrice, DateTime dateOfSale)
        {
            Id = id;
            Client = client;
            Voucher = voucher;
            CountOfVouchers = countOfVouchers;
            TotalPrice = totalPrice;
            DateOfSale = dateOfSale;
        }

        public PaymentCheck CreateFromViewModel()
        {
            return new PaymentCheck(Id, Client, Voucher, CountOfVouchers, TotalPrice, DateOfSale);
        }
    }
}
