using Ontourage.Core.Entities;
using System.Collections.Generic;

namespace Ontourage.Core.Interfaces
{
    public interface IFoodTypeRepository
    {
        List<FoodType> GetAllFoodTypes();

        FoodType GetFoodTypeById(int id);
    }
}