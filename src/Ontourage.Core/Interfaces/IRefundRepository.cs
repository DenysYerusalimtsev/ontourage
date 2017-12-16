using System.Collections.Generic;
using Ontourage.Core.Entities;

namespace Ontourage.Core.Interfaces
{
    public interface IRefundRepository
    {
        List<Refund> GetAllRefunds();

        int AddRefund(PaymentCheck check);

        Refund GetRefundById(int id);
    }
}
