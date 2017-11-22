namespace Ontourage.Core.Entities
{
    public class Country
    {
        public string CountryCode { get; set; }

        public string SetCountry { get; set; }

        public Country(string countryCode, string setCountry)
        {
            CountryCode = countryCode;
            SetCountry = setCountry;
        }
    }
}
