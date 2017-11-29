using Ontourage.Core.Entities;
using System.Collections.Generic;

namespace Ontourage.Core.Interfaces
{
    public interface IPaymentChecksRepository
    {
        List<PaymentCheck> GetAllPaymentChecks();

        PaymentCheck GetPaymentCheckById(int id);

        int AddPaymentCheck(BuyVoucherModel model);

        List<ClientAggregate> GetSameEmailClients(int id);
    }
}
