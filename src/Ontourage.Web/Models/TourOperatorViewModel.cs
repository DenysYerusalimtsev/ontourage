using System.ComponentModel.DataAnnotations;
using Ontourage.Core.Entities;

namespace Ontourage.Web.Models
{
    public class TourOperatorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Туристический оператор")]
        public string TourOperatorName { get; set; }

        public TourOperatorViewModel(int id, string tourOperatorName)
        {
            Id = id;
            TourOperatorName = tourOperatorName;
        }

        public TourOperatorViewModel(TourOperator tourOperator)
            : this(tourOperator.Id, tourOperator.TourOperatorName)
        {
        }
    }
}
