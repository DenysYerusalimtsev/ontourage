using Ontourage.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class HotelViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Нвзвание отеля")]
        [Required(ErrorMessage = "Название отеля является обязательным полем")]
        public string HotelName { get; set; }

        [Display(Name = "Выберите страну")]
        [Required(ErrorMessage = "Выбор страны является обязательным полем")]
        public string CountryCode { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; }

        public List<Country> Countries { get; set; } 

        [Display(Name = "Количество звезд")]
        [Required(ErrorMessage = "Количество звезд является обязательным полем")]
        public int CountOfStars { get; set; }

        public HeaderViewModel Header { get; set; }

        public void BindFromModel(HotelAggregate hotel)
        {
            Id = hotel.Id;
            HotelName = hotel.HotelName;
            CountryCode = hotel.Country.CountryCode;
            CountOfStars = hotel.CountOfStars;
        }

        public Hotel CreateFromModel()
        {
            return new Hotel(Id, HotelName, CountryCode, CountOfStars);
        }
    }
}
