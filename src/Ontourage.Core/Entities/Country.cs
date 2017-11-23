namespace Ontourage.Core.Entities
{
    public class Country
    {
        public string CountryCode { get; }

        public string CountryName { get; }

        public Country(string countryCode, string countryName)
        {
            CountryCode = countryCode;
            CountryName = countryName;
        }
    }
}
