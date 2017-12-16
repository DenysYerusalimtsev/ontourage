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
                    "SELECT r.Id, r.PaymentCheckId, c.Id AS ClientId, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, d.Id AS DiscountId, d.Type AS DiscountType, d.Percantages, c.UserLevel, " +
                    "v.Id AS VoucherId, v.TourName, h.Id AS HotelId, h.HotelName, h.CountOfStars," +
                    "co.Code AS CountryCode, co.Country AS CountryName, v.PassageInclude, f.Id AS FoodId, f.FoodType, " +
                    "t.Id AS TourOperatorId, t.TourOperator AS TourOperatorName, v.Price, v.CountFreeVouchers, " +
                    "v.DepartureTime, v.DeparturePlace, v.ArrivalTime, v.ArrivalPlace, " +
                    "p.CountOfVouchers, p.TotalPrice, p.DateOfSale, p.Status, r.DateOfRefund " +
                    "FROM Refunds r " +
                    "INNER JOIN PaymentChecks p ON r.PaymentCheckId = p.Id " +
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
                    var refund = ReadRefund(reader);
                    refunds.Add(refund);
                }
                return refunds;
            }
        }

        public int AddRefund(PaymentCheck check)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
               "INSERT INTO Refunds (PaymentCheckId, DateOfRefund)" +
                    "OUTPUT INSERTED.ID " +
                    "VALUES (@PaymentCheckId, @DateOfRefund)";

                command.AddParameter("@PaymentCheckId", check.Id);
                command.AddParameter("@DateOfRefund", DateTime.Now);

                int id = (int)command.ExecuteScalar();
                return id;
            }
        }

        public Refund GetRefundById(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
                    "SELECT r.Id, r.PaymentCheckId, c.Id AS ClientId, c.FirstName, c.LastName, c.Sex, c.DateOfBirth, c.Passport, " +
                    "c.PhoneNumber, c.Email, d.Id AS DiscountId, d.Type AS DiscountType, d.Percantages, c.UserLevel, " +
                    "v.Id AS VoucherId, v.TourName, h.Id AS HotelId, h.HotelName, h.CountOfStars," +
                    "co.Code AS CountryCode, co.Country AS CountryName, v.PassageInclude, f.Id AS FoodId, f.FoodType, " +
                    "t.Id AS TourOperatorId, t.TourOperator AS TourOperatorName, v.Price, v.CountFreeVouchers, " +
                    "v.DepartureTime, v.DeparturePlace, v.ArrivalTime, v.ArrivalPlace, " +
                    "p.CountOfVouchers, p.TotalPrice, p.DateOfSale, p.Status, r.DateOfRefund " +
                    "FROM Refunds r " +
                    "INNER JOIN PaymentChecks p ON r.PaymentCheckId = p.Id " +
                    "INNER JOIN Clients c ON p.ClientId = c.Id " +
                    "INNER JOIN Discount d on c.DiscountId = d.Id " +
                    "INNER JOIN Vouchers v ON p.VoucherId = v.Id " +
                    "INNER JOIN Hotels h on v.HotelId = h.Id " +
                    "INNER JOIN Countries co on h.CountryCode = co.Code " +
                    "INNER JOIN Food f ON v.FoodId = f.Id " +
                    "INNER JOIN TourOperators t ON v.TourOperatorId = t.Id " +
                    "WHERE r.Id = @Id";

                command.AddParameter("@Id", id);

                IDataReader reader = command.ExecuteReader();
                return reader.Read() ? ReadRefund(reader) : null;
            }
        }


        private Refund ReadRefund(IDataReader reader)
        {
            return new Refund(
                (int)reader["Id"],
                new PaymentCheck(
                    id: (int)reader["PaymentCheckId"],
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
                checkStatus: (PaymentCheck.Status)reader["Status"]),
                (DateTime)reader["DateOfRefund"]);
        }
    }
}