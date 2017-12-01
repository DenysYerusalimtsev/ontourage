using System;

namespace Ontourage.Web.Models
{
    public class DateSearchViewModel
    {
        public DateTime FirstDate { get; set; }
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
