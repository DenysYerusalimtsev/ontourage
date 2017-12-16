using System;

namespace Ontourage.Core.Entities
{
    public class Refund
    {
        public int Id { get; set; }

        public PaymentCheck PaymentCheck { get; set; }

        public DateTime DateOfRefund { get; set; }

        public Refund(int id, PaymentCheck paymentCheckId, DateTime dateOfRefund)
        {
            Id = id;
            PaymentCheck = paymentCheckId;
            DateOfRefund = dateOfRefund;
        }

        public Refund CreateFromViewModel()
        {
            return new Refund(Id, PaymentCheck, DateOfRefund);
        }
    }
}
