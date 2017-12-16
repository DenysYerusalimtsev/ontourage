using System;
using System.Collections.Generic;
using Ontourage.Core.Interfaces;
using Ontourage.Core.Entities;
using System.Data;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbPaymentChecksRepository : IPaymentChecksRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbPaymentChecksRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<PaymentCheck> GetAllPaymentChecks()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var paymentChecks = new List<PaymentCheck>();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT p.Id, c.Id AS ClientId, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, d.Id AS DiscountId, d.Type AS DiscountType, d.Percantages, c.UserLevel, " +
                    "v.Id AS VoucherId, v.TourName, h.Id AS HotelId, h.HotelName, h.CountOfStars," +
                    "co.Code AS CountryCode, co.Country AS CountryName, v.PassageInclude, f.Id AS FoodId, f.FoodType, " +
                    "t.Id AS TourOperatorId, t.TourOperator AS TourOperatorName, v.Price, v.CountFreeVouchers, " +
                    "v.DepartureTime, v.DeparturePlace, v.ArrivalTime, v.ArrivalPlace, " +
                    "p.CountOfVouchers, p.TotalPrice, p.DateOfSale, p.Status " +
                    "FROM PaymentChecks p " +
                    "INNER JOIN Clients c ON p.ClientId = c.Id " +
                    "INNER JOIN Discount d on c.DiscountId = d.Id " +
                    "INNER JOIN Vouchers v ON p.VoucherId = v.Id " +
                    "INNER JOIN Hotels h on v.HotelId = h.Id " +
                    "INNER JOIN Countries co on h.CountryCode = co.Code " +
                    "INNER JOIN Food f ON v.FoodId = f.Id " +
                    "INNER JOIN TourOperators t ON v.TourOperatorId = t.Id";

                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var paymentCheck = ReadPaymentCheck(reader);
                    paymentChecks.Add(paymentCheck);
                }
                return paymentChecks;
            }
        }

        public PaymentCheck GetPaymentCheckById(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT p.Id, c.Id AS ClientId, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, d.Id AS DiscountId, d.Type AS DiscountType, d.Percantages, c.UserLevel, " +
                    "v.Id AS VoucherId, v.TourName, h.Id AS HotelId, h.HotelName, h.CountOfStars," +
                    "co.Code AS CountryCode, co.Country AS CountryName, v.PassageInclude, f.Id AS FoodId, f.FoodType, " +
                    "t.Id AS TourOperatorId, t.TourOperator AS TourOperatorName, v.Price, v.CountFreeVouchers, " +
                    "v.DepartureTime, v.DeparturePlace, v.ArrivalTime, v.ArrivalPlace, " +
                    "p.CountOfVouchers, p.TotalPrice, p.DateOfSale, p.Status " +
                    "FROM PaymentChecks p " +
                    "INNER JOIN Clients c ON p.ClientId = c.Id " +
                    "INNER JOIN Discount d on c.DiscountId = d.Id " +
                    "INNER JOIN Vouchers v ON p.VoucherId = v.Id " +
                    "INNER JOIN Hotels h on v.HotelId = h.Id " +
                    "INNER JOIN Countries co on h.CountryCode = co.Code " +
                    "INNER JOIN Food f ON v.FoodId = f.Id " +
                    "INNER JOIN TourOperators t ON v.TourOperatorId = t.Id " +
                    "WHERE p.Id = @CheckId";

                command.AddParameter("@CheckId", id);
                IDataReader reader = command.ExecuteReader();
                return reader.Read() ? ReadPaymentCheck(reader) : null;
            }
        }

        public int AddPaymentCheck(BuyVoucherModel model)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "INSERT INTO PaymentChecks (VoucherId, ClientId, CountOfVouchers, TotalPrice, DateOfSale, Status) " +
                    "OUTPUT INSERTED.ID " +
                    "VALUES (@VoucherId, @ClientId, @CountOfVouchers, @TotalPrice, @DateOfSale)";

                command.AddParameter("@VoucherId", model.VoucherId);
                command.AddParameter("@ClientId", model.ClientId);
                command.AddParameter("@CountOfVouchers", model.CountOfVouchers);
                command.AddParameter("@TotalPrice", model.TotalPrice);
                command.AddParameter("@DateOfSale", DateTime.Now);

                int id = (int)command.ExecuteScalar();
                return id;
            }
        }

        public List<ClientAggregate> GetSameEmailClients(int voucherId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var clients = new List<ClientAggregate>();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT c.Id AS ClientId, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, c.DiscountId, d.Type AS DiscountType, d.Percantages, c.UserLevel," +
                    "v.Id AS VoucherId " +
                    "FROM PaymentChecks p " +
                    "INNER JOIN Clients c ON p.ClientId = c.Id " +
                    "INNER JOIN Discount d ON c.DiscountId = d.Id " +
                    "INNER JOIN Vouchers v ON p.VoucherId = v.Id " +
                    "WHERE VoucherId = @VoucherId";

                command.AddParameter("@VoucherId", voucherId);
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var client = ReadClient(reader);
                    clients.Add(client);
                }
                return clients;
            }
        }

        public List<PaymentCheck> GetThisDayChecks()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var checks = new List<PaymentCheck>();

                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT p.Id, c.Id AS ClientId, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, d.Id AS DiscountId, d.Type AS DiscountType, d.Percantages, c.UserLevel, " +
                    "v.Id AS VoucherId, v.TourName, h.Id AS HotelId, h.HotelName, h.CountOfStars," +
                    "co.Code AS CountryCode, co.Country AS CountryName, v.PassageInclude, f.Id AS FoodId, f.FoodType, " +
                    "t.Id AS TourOperatorId, t.TourOperator AS TourOperatorName, v.Price, v.CountFreeVouchers, " +
                    "v.DepartureTime, v.DeparturePlace, v.ArrivalTime, v.ArrivalPlace, " +
                    "p.CountOfVouchers, p.TotalPrice, p.DateOfSale, p.Status " +
                    "FROM PaymentChecks p " +
                    "INNER JOIN Clients c ON p.ClientId = c.Id " +
                    "INNER JOIN Discount d on c.DiscountId = d.Id " +
                    "INNER JOIN Vouchers v ON p.VoucherId = v.Id " +
                    "INNER JOIN Hotels h on v.HotelId = h.Id " +
                    "INNER JOIN Countries co on h.CountryCode = co.Code " +
                    "INNER JOIN Food f ON v.FoodId = f.Id " +
                    "INNER JOIN TourOperators t ON v.TourOperatorId = t.Id " +
                    "WHERE DAY(p.DateOfSale) = DAY(GETDATE())";

                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var check = ReadPaymentCheck(reader);
                    checks.Add(check);
                }
                return checks;
            }
        }

        public void DoRefund(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE PaymentChecks " +
                                      "SET Status = 0 " +
                                      "WHERE Id = @Id";

                command.AddParameter("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        private PaymentCheck ReadPaymentCheck(IDataReader reader)
        {
            return new PaymentCheck(
                id: (int)reader["Id"],
                client: new ClientAggregate(
                    id: (int)reader["ClientId"],
                    firstName: reader["FirstName"].ToString(),
                    lastName: reader["LastName"].ToString(),
                    sex: reader["Sex"].ToString(),
                    dateOfBirth: (DateTime)reader["DateOfBirth"],
                    passport: reader["Passport"].ToString(),
                    phoneNumber: reader["PhoneNumber"].ToString(),
                    email: reader["Email"].ToString(),
                    discount: new Discount(
                        (int)reader["DiscountId"],
                        reader["DiscountType"].ToString(),
                        (int)reader["Percantages"]),
                    userLevel: (int)reader["UserLevel"]),
                voucher: new VoucherAggregate(
                    id: (int)reader["VoucherId"],
                    tourName: reader["TourName"].ToString(),
                    hotel: new HotelAggregate(
                        id: (int)reader["HotelId"],
                        hotelName: reader["HotelName"].ToString(),
                        country: new Country(
                            countryCode: reader["CountryCode"].ToString(),
                            countryName: reader["CountryName"].ToString()),
                        countOfStars: (int)reader["CountOfStars"]),
                    passageInclude: (bool)reader["PassageInclude"],
                    foodType: new FoodType(
                        id: (int)reader["FoodId"],
                        name: reader["FoodType"].ToString()),
                    tourOperator: new TourOperator(
                        id: (int)reader["Id"],
                        tourOperatorName: reader["TourOperatorName"].ToString()),
                    price: (double)reader["Price"],
                    countFreeVouchers: (int)reader["CountFreeVouchers"],
                    departureTime: (DateTime)reader["DepartureTime"],
                    departurePlace: reader["DeparturePlace"].ToString(),
                    arrivalTime: (DateTime)reader["ArrivalTime"],
                    arrivalPlace: reader["ArrivalPlace"].ToString()),
                countOfVouchers: (int)reader["CountOfVouchers"],
                totalPrice: (double)reader["TotalPrice"],
                dateOfSale: (DateTime)reader["DateOfSale"],
                checkStatus: (PaymentCheck.Status)reader["Status"]
            );
        }

        private ClientAggregate ReadClient(IDataReader reader)
        {
            return new ClientAggregate(
                id: (int)reader["ClientId"],
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
