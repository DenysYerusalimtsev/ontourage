using Ontourage.Core.Entities;
using System.Collections.Generic;

namespace Ontourage.Core.Interfaces
{
   public interface ITourOperatorRepository
    {
        List<TourOperator> GetAllTourOperators();
        TourOperator GetTourOperatorById(int id);
    }
}
