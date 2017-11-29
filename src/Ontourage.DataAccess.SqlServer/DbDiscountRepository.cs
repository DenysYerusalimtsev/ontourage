using Ontourage.Core.Interfaces;
using System.Collections.Generic;
using System.Data;
using Ontourage.Core.Entities;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbDiscountRepository : IDiscountRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbDiscountRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<Discount> GetAllDiscounts()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var discounts = new List<Discount>();

                IDbCommand command = connection.CreateCommand();
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
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
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