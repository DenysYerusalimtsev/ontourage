using System.ComponentModel.DataAnnotations;
using Ontourage.Core.Entities;

namespace Ontourage.Web.Models
{
    public class DiscountViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Тип скидки")]
        public string Type { get; set; }

        [Display(Name = "Количество скидки")]
        public int Count { get; set; }

        public DiscountViewModel(int id, string type, int count)
        {
            Id = id;
            Type = type;
            Count = count;
        }

        public DiscountViewModel(Discount discount) 
            : this(discount.Id, discount.Type, discount.Count)
        {
        }
    }
}
