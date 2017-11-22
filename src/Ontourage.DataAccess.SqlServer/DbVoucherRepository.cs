using System;
using System.Collections.Generic;
using Ontourage.Core.Entities;
using Ontourage.Core.Interfaces;
using System.Linq;

namespace Ontourage.DataAccess.SqlServer
{
    public class DbVoucherRepository : IVoucherRepository
    {
        private static List<Voucher> _vouchers = new List<Voucher>
        {
            new Voucher(1, "Солнечная Турция", "TR", 1, true, 1, 1, 1250, 30,
                new DateTime(2008, 3, 1, 7, 0, 0), "Харьков",
                new DateTime(2009, 3, 1, 7, 0, 0), "Анталия"),

                new Voucher(2, "Туманный Альбион", "GB", 2, true, 2, 2, 2250, 15,
                new DateTime(2008, 3, 1, 7, 0, 0), "Харьков",
                new DateTime(2009, 3, 1, 7, 0, 0), "Лондон"),

                new Voucher( 3, "В гости к Дяде Сэму", "US", 3, true, 1, 2, 2500, 17,
                new DateTime(2008, 3, 1, 7, 0, 0), "Харьков",
                new DateTime(2009, 3, 1, 7, 0, 0), "Вашингтон")
        };
        private int _id = 3;
        public void AddVoucher(Voucher voucher)
        {
            _id++;
            _vouchers.Add(voucher);
        }

        public PaymentCheck BuyVoucher(int voucherId, int clientId, int countOfOrderedVouchers,
            double totalPrice)
        {
            Voucher voucher = GetVoucherById(voucherId);
            voucher.CountFreeVouchers--;
            PaymentCheck paymentCheck = new PaymentCheck(_id, clientId, voucherId, countOfOrderedVouchers, 
                totalPrice, DateTime.Now);
            return paymentCheck;
        }

        public void DeleteVoucher(int id)
        {
            var voucherToDelete = GetVoucherById(id);
            _vouchers.Remove(voucherToDelete);
        }

        public Voucher EditVoucher(Voucher voucher)
        {
            DeleteVoucher(voucher.Id);
            AddVoucher(voucher);
            return voucher;
        }

        public List<Voucher> GetAllVouchers()
        {
            return _vouchers;
        }

        public Voucher GetVoucherById(int id)
        {
            return _vouchers.Where(v => v.Id == id).FirstOrDefault();
        }

        public Voucher PrintVoucher()
        {
            throw new NotImplementedException();
        }

        public Voucher ViewDetails(Voucher voucher)
        {
            return voucher;
        }
    }
}
