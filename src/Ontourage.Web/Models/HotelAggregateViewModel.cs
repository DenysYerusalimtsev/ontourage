using Ontourage.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class HotelAggregateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Нвзвание отеля")]
        public string HotelName { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Display(Name = "Количество звезд")]
        public int CountOfStars { get; set; }

        public HeaderViewModel Header { get; set; }


        public void BindFromModel(Hotel hotel)
        {
            Id = hotel.Id;
            HotelName = hotel.HotelName;
            CountOfStars = hotel.CountOfStars;
        }
    }
}
