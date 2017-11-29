using System.Collections.Generic;
using System.Data;
using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbTourOperatorRepository : ITourOperatorRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbTourOperatorRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public List<TourOperator> GetAllTourOperators()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var tourOperators = new List<TourOperator>();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Id, TourOperator FROM TourOperators";
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var tourOperator = ReadTourOperator(reader);
                    tourOperators.Add(tourOperator);
                }
                return tourOperators;
            }
        }

        public TourOperator GetTourOperatorById(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Id, TourOperator FROM TourOperators";
                command.AddParameter("@Id", id);

                IDataReader reader = command.ExecuteReader();
                return reader.Read() ? ReadTourOperator(reader) : null;
            }
        }

        private TourOperator ReadTourOperator(IDataReader reader)
        {
            return new TourOperator(
                id: (int)reader["Id"],
                tourOperatorName: reader["TourOperator"].ToString());
        }
    }
}
