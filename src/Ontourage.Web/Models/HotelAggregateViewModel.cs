using Ontourage.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ontourage.Web.Models
{
    public class HotelAggregateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название отеля")]
        public string HotelName { get; set; }

        [Display(Name = "Страна")]
        public CountryViewModel Country { get; set; }

        [Display(Name = "Количество звезд")]
        public int CountOfStars { get; set; }

        public HeaderViewModel Header { get; set; }

        public void BindFromModel(HotelAggregate hotel)
        {
            Id = hotel.Id;
            HotelName = hotel.HotelName;
            CountOfStars = hotel.CountOfStars;
            Country = new CountryViewModel(hotel.Country);
        }
        public HotelAggregate CreateFromViewModel()
        {
            return new HotelAggregate(Id, HotelName,
                new Country(Country.CountryCode, Country.CountryCode),
                CountOfStars);
        }
    }
}
