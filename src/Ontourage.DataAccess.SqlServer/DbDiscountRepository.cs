using Ontourage.Core.Interfaces;
using System.Collections.Generic;
using System.Data;
using Ontourage.Core.Entities;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbDiscountRepository : IDiscountRepository
    {
        private readonly IDbConnection _dbConnection;

        public DbDiscountRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Discount> GetAllDiscounts()
        {
            using (_dbConnection)
            {
                var discounts = new List<Discount>();

                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "SELECT Id, Type, Percantages FROM Discount";
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var discount = ReadDiscount(reader);
                    discounts.Add(discount);
                }

                return discounts;
            }
        }

        public Discount GetDiscountById(int id)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "SELECT Id, Type, Percantages FROM Discount";

                command.AddParameter("@Id", id);

                IDataReader reader = command.ExecuteReader();
                return reader.Read() ? ReadDiscount(reader) : null;
            }
        }

        private Discount ReadDiscount(IDataReader reader)
        {
            return new Discount(
                id: (int)reader["Id"],
                type: reader["Type"].ToString(),
                count: (int)reader["Percantages"]);
        }
    }
}