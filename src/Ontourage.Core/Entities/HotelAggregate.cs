namespace Ontourage.Core.Entities
{
    public class HotelAggregate
    {
        public int Id { get; set; }

        public string HotelName { get; set; }

        public Country Country { get; set; }

        public int CountOfStars { get; set; }

        public HotelAggregate(int id, string hotelName, Country country, int countOfStars)
        {
            Id = id;
            HotelName = hotelName;
            Country = country;
            CountOfStars = countOfStars;
        }

        public HotelAggregate CreateFromViewModel()
        {
            return new HotelAggregate(Id, HotelName, Country, CountOfStars);
        }
    }
}
