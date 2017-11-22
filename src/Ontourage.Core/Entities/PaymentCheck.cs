using System;

namespace Ontourage.Core.Entities
{
    public class PaymentCheck
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int VoucherId { get; set; }

        public int CountOfVouchers { get; set; }

        public double TotalPrice { get; set; }

        public DateTime DateOfSale { get; set; }

        public PaymentCheck(int id, int clientId, int voucherId, int countOfVouchers, 
            double totalPrice, DateTime dateOfSale)
        {
            Id = id;
            ClientId = clientId;
            VoucherId = voucherId;
            CountOfVouchers = countOfVouchers;
            TotalPrice = totalPrice;
            DateOfSale = dateOfSale;
        }
    }
}
