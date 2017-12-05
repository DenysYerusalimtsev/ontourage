using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class SearchCountryViewModel
    {
        [Display(Name = "Поиск по городам")]

        public string Country { get; set;}

        public SearchCountryViewModel(string country)
        {
            Country = country;
        }

        public SearchCountryViewModel()
        {
        }
    }
}
