using System;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class DateSearchViewModel
    {
        [Display(Name = "С")]
        public DateTime DepartuteDate { get; set; }

        [Display(Name = "По")]
        public DateTime ArrivalDate { get; set; }

        public DateSearchViewModel()
        {
        }

        public DateSearchViewModel(DateTime departuteDate, DateTime arrivalDate)
        {
            DepartuteDate = departuteDate;
            ArrivalDate = arrivalDate;
        }
    }
}
