using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbHotelRepository : IHotelRepository
    {
        private readonly IDbConnection _dbConnection;

        public DbHotelRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Hotel> GetAllHotels()
        {
            using (_dbConnection)
            {
                var hotels = new List<Hotel>();

                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "SELECT Id, HotelName, CountryCode, CountOfStars FROM Hotels";
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var hotel = ReadHotel(reader);
                    hotels.Add(hotel);
                }
                return hotels;
            }
        }
        public Hotel GetHotelById(int id)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "SELECT Id, HotelName, CountryCode, CountOfStars " +
                                      "FROM Hotels " +
                                      "WHERE Id = @Id";

                command.AddParameter("@Id", id);

                IDataReader reader = command.ExecuteReader();
                return reader.Read() ? ReadHotel(reader) : null;
            }
        }

        public void AddHotel(Hotel hotel)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "INSERT INTO Hotels (HotelName, CountryCode, CountOfStars) " +
                                      "VALUES (@HotelName, @CountryCode, @CountOfStars)";

                command.AddParameter("@HotelName", hotel.HotelName);
                command.AddParameter("@CountryCode", hotel.CountryCode);
                command.AddParameter("@CountOfStars", hotel.CountOfStars);

                command.ExecuteNonQuery();
            }
        }

        public void EditHotel(Hotel hotel)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "UPDATE Hotels SET " +
                                      "HotelName = @HotelName, " +
                                      "CountryCode = @CountryCode, " +
                                      "CountOfStars = @CountOfStars";

                command.AddParameter("@HotelName", hotel.HotelName);
                command.AddParameter("@CountryCode", hotel.CountryCode);
                command.AddParameter("@CountOfStars", hotel.CountOfStars);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteHotel(int id)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "DELETE FROM Hotels " +
                                      "WHERE Id = @Id";

                command.AddParameter("@Id", id);

                command.ExecuteNonQuery();
            }
        }

        private Hotel ReadHotel(IDataReader reader)
        {
            return new Hotel(
                id: (int)reader["Id"],
                hotelName: reader["HotelName"].ToString(),
                countryCode: reader["CountryCode"].ToString(),
                countOfStars: (int)reader["CountOfStars"]);
        }
    }
}
