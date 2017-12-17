using System;
using Ontourage.Core.Interfaces;
using System.Collections.Generic;
using System.Data;
using Ontourage.Core.Entities;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbClientRepository : IClientRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbClientRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void AddNewClient(Client client)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Clients (FirstName, LastName, Sex, DateOfBirth, Passport, " +
                                      "PhoneNumber, Email, DiscountId, UserLevel) " +
                                      "VALUES (@FirstName, @LastName, @Sex, @DateOfBirth, @Passport, " +
                                      "@PhoneNumber, @Email, @DiscountId, @UserLevel)";

                command.AddParameter("@Id", client.Id);
                command.AddParameter("@FirstName", client.FirstName);
                command.AddParameter("@LastName", client.LastName);
                command.AddParameter("@Sex", client.Sex);
                command.AddParameter("@DateOfBirth", client.DateOfBirth);
                command.AddParameter("@Passport", client.Passport);
                command.AddParameter("@PhoneNumber", client.PhoneNumber);
                command.AddParameter("@Email", client.Email);
                command.AddParameter("@DiscountId", client.DiscountId);
                command.AddParameter("@UserLevel", client.UserLevel);

                command.ExecuteNonQuery();
            }
        }

        public List<ClientAggregate> SearchClients(string search)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var clients = new List<ClientAggregate>();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT c.Id, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, c.DiscountId, d.Type AS DiscountType, d.Percantages, c.UserLevel " +
                    "FROM Clients c " +
                    "INNER JOIN Discount d ON c.DiscountId = d.Id " +
                    "WHERE LOWER(c.FirstName) LIKE LOWER(@Search) OR " +
                    "LOWER(c.LastName) LIKE LOWER(@Search)";

                command.AddParameter("@Search", search);

                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var client = ReadClient(reader);
                    clients.Add(client);
                }
                return clients;
            }
        }

        public void DeleteClient(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Clients " +
                                      "WHERE Id = @Id";

                command.AddParameter("@Id", id);

                command.ExecuteNonQuery();
            }
        }

        public void EditClient(Client client)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Clients SET " +
                                      "FirstName = @FirstName, " +
                                      "LastName = @LastName, " +
                                      "Sex = @Sex, " +
                                      "DateOfBirth = @DateOfBirth, " +
                                      "Passport = @Passport, " +
                                      "PhoneNumber = @PhoneNumber, " +
                                      "Email = @Email, " +
                                      "DiscountId = @DiscountId, " +
                                      "UserLevel = @UserLevel " +
                                      "WHERE Id = @Id";

                command.AddParameter("@Id", client.Id);
                command.AddParameter("@FirstName", client.FirstName);
                command.AddParameter("@LastName", client.LastName);
                command.AddParameter("@Sex", client.Sex);
                command.AddParameter("@DateOfBirth", client.DateOfBirth);
                command.AddParameter("@Passport", client.Passport);
                command.AddParameter("@PhoneNumber", client.PhoneNumber);
                command.AddParameter("@Email", client.Email);
                command.AddParameter("@DiscountId", client.DiscountId);
                command.AddParameter("@UserLevel", client.UserLevel);

                command.ExecuteNonQuery();
            }
        }

        public List<ClientAggregate> GetAllClients()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var clients = new List<ClientAggregate>();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT c.Id, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, c.DiscountId, d.Type AS DiscountType, d.Percantages, c.UserLevel " +
                    "FROM Clients c " +
                    "INNER JOIN Discount d ON c.DiscountId = d.Id";
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var client = ReadClient(reader);
                    clients.Add(client);
                }
                return clients;
            }
        }


        public List<ClientAggregate> GetReguralClients()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var clients = new List<ClientAggregate>();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT c.Id, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, c.DiscountId, d.Type AS DiscountType, d.Percantages, c.UserLevel " +
                    "FROM Clients c " +
                    "INNER JOIN Discount d ON c.DiscountId = d.Id " +
                    "WHERE c.UserLevel > 10";
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var client = ReadClient(reader);
                    clients.Add(client);
                }
                return clients;
            }
        }

        public ClientAggregate GetClientById(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT c.Id, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, c.DiscountId, d.Type AS DiscountType, d.Percantages, " +
                    "c.UserLevel " +
                    "FROM Clients c " +
                    "INNER JOIN Discount d ON c.DiscountId = d.Id " +
                    "WHERE c.Id = @Id";

                command.AddParameter("@Id", id);

                IDataReader reader = command.ExecuteReader();
                return reader.Read() ? ReadClient(reader) : null;
            }
        }
        public Client ViewDetails(Client client)
        {
            return client;
        }

        private ClientAggregate ReadClient(IDataReader reader)
        {
            return new ClientAggregate(
                id: (int)reader["Id"],
                firstName: reader["FirstName"].ToString(),
                lastName: reader["LastName"].ToString(),
                sex: reader["Sex"].ToString(),
                dateOfBirth: (DateTime)reader["DateOfBirth"],
                passport: reader["Passport"].ToString(),
                phoneNumber: reader["PhoneNumber"].ToString(),
                email: reader["Email"].ToString(),
                discount: new Discount(
                    id: (int)reader["DiscountId"],
                    type: reader["DiscountType"].ToString(),
                    count: (int)reader["Percantages"]),
                userLevel: (int)reader["UserLevel"]);
        }
    }
}