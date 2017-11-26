using System;
using System.Collections.Generic;
using Ontourage.Core.Interfaces;
using Ontourage.Core.Entities;
using System.Data;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbPaymentChecksRepository : IPaymentChecksRepository
    {
        private readonly IDbConnection _dbConnection;

        public DbPaymentChecksRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<PaymentCheck> GetAllPaymentChecks()
        {
            using (_dbConnection)
            {
                var paymentChecks = new List<PaymentCheck>();

                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText =
                    "SELECT p.Id, v.Id AS VoucherId, c.Id AS ClientId, p.CountOfVouchers, " +
                    "p.TotalPrice, p.DateOfSale " +
                    "FROM PaymentChecks p " +
                    "INNER JOIN Vouchers v ON p.VoucherId = v.Id " +
                    "INNER JOIN Clients c ON p.ClientId = c.Id";

                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var paymentCheck = ReadPaymentCheck(reader);
                    paymentChecks.Add(paymentCheck);
                }
                return paymentChecks;
            }
        }

        public PaymentCheck ViewDetails(int id)
        {
            return GetPaymentCheckById(id);
        }

        public PaymentCheck GetPaymentCheckById(int id)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText =
                    "SELECT p.Id, v.Id AS VoucherId, c.Id AS ClientId, p.CountOfVouchers, " +
                    "p.TotalPrice, p.DateOfSale " +
                    "FROM PaymentChecks p " +
                    "INNER JOIN Vouchers v ON p.VoucherId = v.Id " +
                    "INNER JOIN Clients c ON p.ClientId = c.Id";

                IDataReader reader = command.ExecuteReader();
                return reader.Read() ? ReadPaymentCheck(reader) : null;
            }
        }

        public void AddPaymentCheck(PaymentCheck paymentCheck)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText =
                    "INSERT INTO PaymentChecks (VoucherId, ClientId, CountOfVouchers, TotalPrice, DateOfSale, " +
                    "VALUES (@VoucherId, @ClientId, @CountOfVouchers, @TotalPrice, @DateOfSale";

                command.AddParameter("@VoucherId", paymentCheck.Voucher.Id);
                command.AddParameter("@ClientId", paymentCheck.Client.Id);
                command.AddParameter("@CountOfVouchers", paymentCheck.CountOfVouchers);
                command.AddParameter("@TotalPrice", paymentCheck.TotalPrice);
                command.AddParameter("@DateOfSale", paymentCheck.DateOfSale);


                command.ExecuteNonQuery();
            }

        }

        private PaymentCheck ReadPaymentCheck(IDataReader reader)
        {
            return new PaymentCheck(
                id: (int)reader["Id"],
                client: new ClientAggregate(
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
                    userLevel: (int)reader["UserLevel"]),
                    voucher: new VoucherAggregate(
                    id: (int)reader["Id"],
                    tourName: reader["TourName"].ToString(),
                    hotel: new HotelAggregate(
                        id: (int)reader["Id"],
                        hotelName: reader["HotelName"].ToString(),
                        country: new Country(
                            countryCode: reader["CountryCode"].ToString(),
                            countryName: reader["CountryName"].ToString()),
                        countOfStars: (int)reader["CountOfStars"]),
                    passageInclude: (bool)reader["PassageInclude"],
                    foodType: new FoodType(
                        id: (int)reader["Id"],
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
                    dateOfSale: (DateTime)reader["DateOfSale"]
                    );
        }
    }
}
