using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class SearchByName
    {
        [Display(Name = "Введите название страны")]
        public string Name { get;  set; }

        public SearchByName()
        {
        }

        public SearchByName(string name)
        {
            Name = name;
        }
    }
}
