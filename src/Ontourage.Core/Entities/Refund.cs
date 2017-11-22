using System;

namespace Ontourage.Core.Entities
{
    public class Refund
    {
        int Id { get; set; }

        int PaymentCheckId { get; set; }

        DateTime DateOfRefund { get; set; }

        public Refund(int id, int paymentCheckId, DateTime dateOfRefund)
        {
            Id = id;
            PaymentCheckId = paymentCheckId;
            DateOfRefund = dateOfRefund;
        }
    }
}
