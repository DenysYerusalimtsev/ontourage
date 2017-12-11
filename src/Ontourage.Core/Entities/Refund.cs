using System;

namespace Ontourage.Core.Entities
{
    public class Refund
    {
        public int Id { get; set; }

        public int PaymentCheckId { get; set; }

        public DateTime DateOfRefund { get; set; }

        public Refund(int id, int paymentCheckId, DateTime dateOfRefund)
        {
            Id = id;
            PaymentCheckId = paymentCheckId;
            DateOfRefund = dateOfRefund;
        }
    }
}
