namespace Ontourage.Core.Entities
{
    public class Hotel
    {
        public int Id { get; set; }

        public string HotelName { get; set; }

        public string CountryCode { get; set; }

        public int CountOfStars { get; set; }

        public Hotel(int id, string hotelName, string countryCode, int countOfStars)
        {
            Id = id;
            HotelName = hotelName;
            CountryCode = countryCode;
            CountOfStars = countOfStars;
        }
    }
}
