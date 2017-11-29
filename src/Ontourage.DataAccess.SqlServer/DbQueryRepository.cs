using System;
using System.Collections.Generic;
using System.Data;
using Ontourage.Core.Entities.Sql;
using Ontourage.Core.Interfaces;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbQueryRepository : IDbQueryRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbQueryRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public SelectResult ExecuteQuery(string query)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = query;

                IDataReader reader = command.ExecuteReader();

                var resultList = new List<List<string>>();
                while (reader.Read())
                {
                    var row = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string value = reader[i] == DBNull.Value
                            ? "NULL"
                            : reader[i].ToString();
                        row.Add(value);
                    }
                    resultList.Add(row);
                }

                return new SelectResult(resultList);
            }
        }

        public void ExecuteNonQuery(string commandText)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = commandText;

                command.ExecuteNonQuery();
            }
        }
    }
}
