using System;
using System.Collections.Generic;
using System.Data;
using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbRefundRepository : IRefundRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbRefundRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<Refund> GetAllRefunds()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var refunds = new List<Refund>();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT r.Id, r.PaymentCheckId, c.Id AS ClientId, c.FirstName, c.LastName, " +
                    "v.TourName, p.DateOfSale, r.DateOfRefund " +
                    "FROM Refunds r " +
                    "INNER JOIN PaymentChecks p ON r.PaymentCheckId = p.Id " +
                    "INNER JOIN Clients c ON p.ClientId = c.Id " +
                    "INNER JOIN Vouchers v ON p.VoucherId = v.Id ";
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var refund = ReadRefund(reader);
                    refunds.Add(refund);
                }
                return refunds;
            }
        }

        public void AddRefund(PaymentCheck check)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
               "INSERT INTO Refunds (PaymentCheckId, DateOfRefund)" +
                    "VALUES (@PaymentCheckId, @DateOfRefund)";

                command.AddParameter("@PaymentCheckId", check.Id);
                command.AddParameter("@DateOfRefund", DateTime.Now);
            }
        }


        private Refund ReadRefund(IDataReader reader)
        {
            return new Refund(
                id: (int)reader["Id"],
                paymentCheckId: (int)reader["PaymentCheckId"],
                dateOfRefund: (DateTime)reader["DateOfRefund"]);
        }
    }
}