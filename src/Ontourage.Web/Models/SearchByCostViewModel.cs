using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class SearchByCostViewModel
    {
        [Display(Name = "Поиск по цене")]
        public double Price { get; set; }

        public SearchByCostViewModel()
        {
        }

        public SearchByCostViewModel(double price)
        {
            Price = price;
        }
    }
}
