using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbFoodTypesRepository : IFoodTypeRepository
    {
        private readonly IDbConnection _dbConnection;

        public DbFoodTypesRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public List<FoodType> GetAllFoodTypes()
        {
            using (_dbConnection)
            {
                var foodTypes = new List<FoodType>();

                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
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
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
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
