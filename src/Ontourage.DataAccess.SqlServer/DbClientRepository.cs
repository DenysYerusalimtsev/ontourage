using System;
using Ontourage.Core.Interfaces;
using System.Collections.Generic;
using System.Data;
using Ontourage.Core.Entities;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbClientRepository : IClientRepository
    {
        private readonly IDbConnection _dbConnection;

        public DbClientRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void AddNewClient(Client client)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "INSERT INTO Clients (FirstName, LastName, Sex, DateOfBirth, Passport, " +
                                      "PhoneNumber, Email, DicountId, UserLevel) " +
                                      "VALUES (@FirstName, @LastName, @Sex, @DateOfBirth, @Passport, " +
                                      "@PhoneNumber, @Email, @DicountId, @UserLevel)";

                command.AddParameter("@Id", client.Id);
                command.AddParameter("@FirstName", client.FirstName);
                command.AddParameter("@LastName", client.LastName);
                command.AddParameter("@Sex", client.Sex);
                command.AddParameter("@DateOfBirth", client.DateOfBirth);
                command.AddParameter("@Passport", client.Passport);
                command.AddParameter("@PhoneNumber", client.PhoneNumber);
                command.AddParameter("@Email", client.Email);
                command.AddParameter("@DicountId", client.DiscountId);
                command.AddParameter("@UserLevel", client.UserLevel);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteClient(int id)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "DELETE FROM Clients " +
                                      "WHERE Id = @Id";

                command.AddParameter("@Id", id);

                command.ExecuteNonQuery();
            }
        }

        public void EditClient(Client client)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "UPDATE Clients SET " +
                                      "FirstName = @FirstName, " +
                                      "LastName = @LastName, " +
                                      "Sex = @Sex " +
                                      "DateOfBirth = @DateOfBirth " +
                                      "Passport = @Passport " +
                                      "PhoneNumber = @PhoneNumber " +
                                      "Email = @Email " +
                                      "DiscountId = @DiscountId " +
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
                command.AddParameter("@DicountId", client.DiscountId);
                command.AddParameter("@UserLevel", client.UserLevel);

                command.ExecuteNonQuery();
            }
        }

        public List<ClientAggregate> GetAllClients()
        {
            using (_dbConnection)
            {
                var clients = new List<ClientAggregate>();

                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText =
                    "SELECT c.Id, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, c.DiscountId, c.UserLevel " +
                    "FROM Clients c " +
                    "INNER JOIN Discounts d ON c.DiscountId = d.Id";
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
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText =
                    "SELECT c.Id, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, c.DiscountId, c.UserLevel " +
                    "FROM Clients c " +
                    "INNER JOIN Discounts d ON c.DiscountId = d.Id";


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
                dateOfBirth: (DateTime)reader["DateOfBitrth"],
                passport: reader["Passport"].ToString(),
                phoneNumber: reader["PhoneNumber"].ToString(),
                email: reader["Email"].ToString(),
                discount: new Discount(
                    (int)reader["Id"],
                    reader["Type"].ToString(),
                    (int)reader["Count"]),
                userLevel: (int)reader["UserLevel"]);
        }
    }
}