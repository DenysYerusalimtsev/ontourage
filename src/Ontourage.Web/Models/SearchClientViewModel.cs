using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class SearchClientViewModel
    {
        [Display(Name = "Поиск по клиентам")]

        public string Client { get; set; }

        public SearchClientViewModel(string client)
        {
            Client = client;
        }

        public SearchClientViewModel()
        {
        }
    }
}
