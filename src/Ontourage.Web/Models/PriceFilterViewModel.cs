using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class PriceFilterViewModel
    {
        [Display(Name = "С")]
        public double CostFrom { get; set; }

        [Display(Name = "По")]
        public double CostTo { get; set; }
    }
}
