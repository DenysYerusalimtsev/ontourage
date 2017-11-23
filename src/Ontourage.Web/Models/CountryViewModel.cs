using System.ComponentModel.DataAnnotations;
using Ontourage.Core.Entities;

namespace Ontourage.Web.Models
{
    public class CountryViewModel
    {
        [Display(Name = "Название страны")]
        public string CountryCode { get; set; }

        [Display(Name = "Название страны")]
        public string CountryName { get; set; }

        public CountryViewModel(string countryCode, string countryName)
        {
            CountryCode = countryCode;
            CountryName = countryName;
        }

        public CountryViewModel(Country country)
            : this(country.CountryCode, country.CountryName)
        {
        }
    }
}
