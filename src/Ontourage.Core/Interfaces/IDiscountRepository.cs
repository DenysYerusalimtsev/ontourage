using Ontourage.Core.Entities;
using System.Collections.Generic;

namespace Ontourage.Core.Interfaces
{
    public interface IDiscountRepository
    {
        List<Discount> GetAllDiscounts();

        Discount GetDiscountById(int id);
    }
}
