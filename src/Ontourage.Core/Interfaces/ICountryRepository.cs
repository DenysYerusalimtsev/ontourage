using Ontourage.Core.Entities;
using System.Collections.Generic;

namespace Ontourage.Core.Interfaces
{
    public interface ICountryRepository
    {
        List<Country> GetAllCoutries();

        Country GetCountryByCode(string code);
    }
}
