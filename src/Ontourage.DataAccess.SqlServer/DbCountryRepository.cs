using System.Collections.Generic;
using System.Data;
using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbCountryRepository : ICountryRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbCountryRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<Country> GetAllCoutries()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var countries = new List<Country>();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Code, Country FROM Countries " +
                                      "ORDER BY Country ASC";
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var country = ReadCountry(reader);
                    countries.Add(country);
                }
                return countries;
            }
        }
        public Country GetCountryByCode(string code)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Code, Country FROM Countries";

                command.AddParameter("@CountryCode", code);

                IDataReader reader = command.ExecuteReader();
                return reader.Read() ? ReadCountry(reader) : null;
            }
        }

        private Country ReadCountry(IDataReader reader)
        {
            return new Country(
                countryCode: reader["Code"].ToString(),
                countryName: reader["Country"].ToString());
        }
    }
}
