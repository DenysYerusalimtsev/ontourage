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
                    var voucher = ReadPaymentCheck(reader);
                    vouchers.Add(voucher);
                }
                return vouchers;
            }
        }

        public PaymentCheck ViewDetails(PaymentCheck paymentCheck)
        {
            return paymentCheck;
        }

        public PaymentCheck GetPaymentCheckById(int id)
        {
            return _paymentChecks.FirstOrDefault(p => p.Id == id);
        }

        public void AddPaymentCheck(PaymentCheck paymentCheck)
        {
            _id++;
            _paymentChecks.Add(paymentCheck);
        }

        private PaymentCheck ReadPaymentCheck(IDataReader reader)
        {
            return new PaymentCheck(
                id: (int) reader["Id"],
                client: new ClientAggregate(
                    id: (int) reader["Id"],
                    firstName: reader["FirstName"].ToString(),
                    lastName: reader["LastName"].ToString(),
                    sex: reader["Sex"].ToString(),
                    dateOfBirth: (DateTime) reader["DateOfBitrth"],
                    passport: reader["Passport"].ToString(),
                    phoneNumber: reader["PhoneNumber"].ToString(),
                    email: reader["Email"].ToString(),
                    discount: new Discount(
                        (int) reader["Id"],
                        reader["Type"].ToString(),
                        (int) reader["Count"]),
                    userLevel: (int) reader["UserLevel"]), 
                    voucher: new VoucherAggregate(
                    id: (int) reader["Id"],
                    tourName: reader["TourName"].ToString(),
                    hotel: new HotelAggregate(
                        id: (int) reader["Id"],
                        hotelName: reader["HotelName"].ToString(),
                        country: new Country(
                            countryCode: reader["CountryCode"].ToString(),
                            countryName: reader["CountryName"].ToString()),
                        countOfStars: (int) reader["CountOfStars"]),
                    passageInclude: (bool) reader["PassageInclude"],
                    foodType: new FoodType(
                        id: (int) reader["Id"],
                        name: reader["FoodType"].ToString()),
                    tourOperator: new TourOperator(
                        id: (int) reader["Id"],
                        tourOperatorName: reader["TourOperatorName"].ToString()),
                    price: (double) reader["Price"],
                    countFreeVouchers: (int) reader["CountFreeVouchers"],
                    departureTime: (DateTime) reader["DepartureTime"],
                    departurePlace: reader["DeparturePlace"].ToString(),
                    arrivalTime: (DateTime) reader["ArrivalTime"],
                    arrivalPlace: reader["ArrivalPlace"].ToString()
                ));
        }
    }
}
