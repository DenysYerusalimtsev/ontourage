using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbFoodTypesRepository : IFoodTypeRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbFoodTypesRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<FoodType> GetAllFoodTypes()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var foodTypes = new List<FoodType>();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Id, FoodType FROM Food";
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var foodType = ReadFoodType(reader);
                    foodTypes.Add(foodType);
                }
                return foodTypes;
            }
        }

        public FoodType GetFoodTypeById(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Id, FoodType FROM Food";

                command.AddParameter("@Id", id);
                IDataReader reader = command.ExecuteReader();
                return reader.Read() ? ReadFoodType(reader) : null;
            }
        }

        private FoodType ReadFoodType(IDataReader reader)
        {
            return new FoodType(
                id: (int)reader["Id"],
                name: reader["FoodType"].ToString());
        }
    }
}
