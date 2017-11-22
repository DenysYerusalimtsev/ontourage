using System.Collections.Generic;
using System.Data;
using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbTourOperatorRepository : ITourOperatorRepository
    {
        private readonly IDbConnection _dbConnection;

        public DbTourOperatorRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public List<TourOperator> GetAllTourOperators()
        {
            using (_dbConnection)
            {
                var tourOperators = new List<TourOperator>();

                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
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
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
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
