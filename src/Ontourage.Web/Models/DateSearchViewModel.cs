using System;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class DateSearchViewModel
    {
        [Display(Name = "С")]
        public DateTime FirstDate { get; set; }

        [Display(Name = "По")]
        public DateTime SecondDate { get; set; }

        public DateSearchViewModel()
        {
        }

        public DateSearchViewModel(DateTime firstDate, DateTime secondDate)
        {
            FirstDate = firstDate;
            SecondDate = secondDate;
        }
    }
}
