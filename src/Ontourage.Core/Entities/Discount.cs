using System.ComponentModel.DataAnnotations;

namespace Ontourage.Core.Entities
{
    public class Discount
    {
        public int Id { get; set; }

        [Display(Name = "Тип скидки")]
        public string Type { get; set; }

        public int Count { get; set; }

        public Discount(int id, string type, int count)
        {
            Id = id;
            Type = type;
            Count = count;
        }
    }
}
