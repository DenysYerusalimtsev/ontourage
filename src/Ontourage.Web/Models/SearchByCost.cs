using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class SearchByCost
    {
        [Display(Name =  "Поиск по цене")]
        public  double Price { get; set; }

        public SearchByCost()
        {
        }

        public SearchByCost(double price)
        {
            Price = price;
        }
    }
}
