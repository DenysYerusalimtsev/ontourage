using System;
using System.Collections.Generic;
using System.Data;
using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbVoucherRepository : IVoucherRepository
    {
        private readonly IDbConnection _dbConnection;

        public DbVoucherRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        public void AddVoucher(Voucher voucher)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "INSERT INTO Vouchers (TourName, CountryCode, HotelId, PassageInclude, FoodId, " +
                                      "TourOperatorId, Price, CountFreeVouchers, DepartureTime, DeparturePlace, " +
                                      "ArrivalTime, ArrivalPlace) " +
                                      "VALUES (@TourName, @CountryCode, @HotelId, @PassageInclude, @FoodId, " +
                                      "@TourOperatorId, @Price, @CountFreeVouchers, @DepartureTime, @DeparturePlace, " +
                                      "@ArrivalTime, @ArrivalPlace)";

                command.AddParameter("@TourName", voucher.TourName);
                command.AddParameter("@CountryCode", voucher.CountryCode);
                command.AddParameter("@HotelId", voucher.HotelId);
                command.AddParameter("@PassageInclude", voucher.PassageInclude);
                command.AddParameter("@FoodId", voucher.FoodTypeId);
                command.AddParameter("@TourOperatorId", voucher.TourOperatorId);
                command.AddParameter("@Price", voucher.Price);
                command.AddParameter("@CountFreeVouchers", voucher.CountFreeVouchers);
                command.AddParameter("@DepartureTime", voucher.DepartureTime);
                command.AddParameter("@DeparturePlace", voucher.DeparturePlace);
                command.AddParameter("@ArrivalTime", voucher.ArrivalTime);
                command.AddParameter("@ArrivalPlace", voucher.ArrivalPlace);


                command.ExecuteNonQuery();
            }
        }

        public void BuyVoucher(BuyVoucherModel model)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "UPDATE Vouchers SET " +
                                      "CountFreeVouchers = CountFreeVouchers - @CountOfOrderedVouchers " +
                                      "WHERE Id = @Id";

                command.AddParameter("@Id", model.VoucherId);
                command.AddParameter("@CountOfOrderedVouchers", model.CountOfVouchers);
                
                command.ExecuteNonQuery();
            }
        }

        public void DeleteVoucher(int id)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "DELETE FROM Vouchers " +
                                      "WHERE Id = @Id";

                command.AddParameter("@Id", id);

                command.ExecuteNonQuery();
            }
        }

        public void EditVoucher(Voucher voucher)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText = "UPDATE Vouchers SET " +
                                      "TourName = @TourName, " +
                                      "CountryCode = @CountryCode," +
                                      "HotelId = @HotelId, " +
                                      "PassageInclude = @PassageInclude, " +
                                      "FoodId = @FoodId, " +
                                      "TourOperatorId = @TourOperatorId, " +
                                      "Price = @Price, " +
                                      "CountFreeVouchers = @CountFreeVouchers, " +
                                      "DepartureTime = @DepartureTime, " +
                                      "DeparturePlace = @DeparturePlace, " +
                                      "ArrivalTime = @ArrivalTime, " +
                                      "ArrivalPlace = @ArrivalPlace " +
                                      "WHERE Id = @Id";

                command.AddParameter("@Id", voucher.Id);
                command.AddParameter("@TourName", voucher.TourName);
                command.AddParameter("@CountryCode", voucher.CountryCode);
                command.AddParameter("@HotelId", voucher.HotelId);
                command.AddParameter("@PassageInclude", voucher.PassageInclude);
                command.AddParameter("@FoodId", voucher.FoodTypeId);
                command.AddParameter("@TourOperatorId", voucher.TourOperatorId);
                command.AddParameter("@Price", voucher.Price);
                command.AddParameter("@CountFreeVouchers", voucher.CountFreeVouchers);
                command.AddParameter("@DepartureTime", voucher.DepartureTime);
                command.AddParameter("@DeparturePlace", voucher.DeparturePlace);
                command.AddParameter("@ArrivalTime", voucher.ArrivalTime);
                command.AddParameter("@ArrivalPlace", voucher.ArrivalPlace);

                command.ExecuteNonQuery();
            }
        }
        
        public List<VoucherAggregate> GetAllVouchers()
        {
            using (_dbConnection)
            {
                var vouchers = new List<VoucherAggregate>();

                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText =
                    "SELECT v.Id, v.TourName, c.Code AS CountryCode, c.Country AS CountryName, " +
                    "h.Id AS HotelId, h.HotelName, h.CountOfStars, v.PassageInclude, f.Id AS FoodId, f.FoodType, " +
                    "t.Id AS TourOperatorId, t.TourOperator AS TourOperatorName, " +
                    "v.Price, v.CountFreeVouchers, v.DepartureTime, v.DeparturePlace, " +
                    "v.ArrivalTime, v.ArrivalPlace " +
                    "FROM Vouchers v " +
                    "INNER JOIN Hotels h ON v.HotelId = h.Id " +
                    "INNER JOIN Countries c ON h.CountryCode = c.Code " +
                    "INNER JOIN Food f ON v.FoodId = f.Id " +
                    "INNER JOIN TourOperators t ON v.TourOperatorId = t.Id";
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var voucher = ReadVoucher(reader);
                    vouchers.Add(voucher);
                }
                return vouchers;
            }
        }
        
        public VoucherAggregate GetVoucherById(int id)
         {
            using (_dbConnection)
            {
                _dbConnection.Open();
                IDbCommand command = _dbConnection.CreateCommand();
                command.CommandText =
                    "SELECT v.Id, v.TourName, c.Code AS CountryCode, c.Country AS CountryName, " +
                    "h.Id AS HotelId, h.HotelName, h.CountOfStars, v.PassageInclude, f.Id AS FoodId, f.FoodType, " +
                    "t.Id AS TourOperatorId, t.TourOperator AS TourOperatorName, " +
                    "v.Price, v.CountFreeVouchers, v.DepartureTime, v.DeparturePlace, " +
                    "v.ArrivalTime, v.ArrivalPlace " +
                    "FROM Vouchers v " +
                    "INNER JOIN Hotels h ON v.HotelId = h.Id " +
                    "INNER JOIN Countries c ON h.CountryCode = c.Code " +
                    "INNER JOIN Food f ON v.FoodId = f.Id " +
                    "INNER JOIN TourOperators t ON v.TourOperatorId = t.Id " +
                    "WHERE v.Id = @Id";

                command.AddParameter("@Id", id);

                IDataReader reader = command.ExecuteReader();
                return reader.Read() ? ReadVoucher(reader) : null;
            }
        }

        public VoucherAggregate ViewDetails(int id)
        {
            return GetVoucherById(id);
        }
        
        private VoucherAggregate ReadVoucher(IDataReader reader)
        {
            return new VoucherAggregate(
                id: (int)reader["Id"],
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
                    id: (int)reader["TourOperatorId"],
                    tourOperatorName: reader["TourOperatorName"].ToString()),
                price: (double)reader["Price"],
                countFreeVouchers: (int)reader["CountFreeVouchers"],
                departureTime: (DateTime)reader["DepartureTime"],
                departurePlace: reader["DeparturePlace"].ToString(),
                arrivalTime: (DateTime)reader["ArrivalTime"],
                arrivalPlace: reader["ArrivalPlace"].ToString()
                );
        }
    }
}