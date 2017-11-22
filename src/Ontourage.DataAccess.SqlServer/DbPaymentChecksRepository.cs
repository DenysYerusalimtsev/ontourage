using System.Collections.Generic;
using Ontourage.Core.Interfaces;
using Ontourage.Core.Entities;
using System;
using System.Linq;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbPaymentChecksRepository : IPaymentChecksRepository
    {
        private  static List<PaymentCheck> _paymentChecks = new List<PaymentCheck>
        {
            new PaymentCheck(1, 2, 1, 1, 1250, new DateTime(2009, 3, 1, 7, 0, 0)),
            new PaymentCheck(2, 3, 2, 1, 1250, new DateTime(2009, 3, 1, 7, 0, 0))
        };

        private int _id = 22;


        public List<PaymentCheck> GetAllPaymentChecks()
        {
            return _paymentChecks;
        }

        public PaymentCheck ViewDetails(PaymentCheck paymentCheck)
        {
            return paymentCheck;
        }

        public PaymentCheck GetPaymentCheckById(int id)
        {
            return _paymentChecks.Where(p => p.Id == id).FirstOrDefault();
        }

        public void AddPaymentCheck(PaymentCheck paymentCheck)
        {
            _id++;
            _paymentChecks.Add(paymentCheck);
        }
    }
}
