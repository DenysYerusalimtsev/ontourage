using Ontourage.Core.Entities;
using System.Collections.Generic;

namespace Ontourage.Core.Interfaces
{
    public interface IRefundsRepository
    {
        Refund CreateNewRefund();

        List<Refund> GetAllRefund();
    }
}
