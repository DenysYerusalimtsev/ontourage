using Ontourage.Core.Entities;
using System.Collections.Generic;

namespace Ontourage.Core.Interfaces
{
    public interface IVoucherRepository
    {
        List<Voucher> GetAllVouchers();

        PaymentCheck BuyVoucher(int voucherId, int clientId, int countOfOrderedVouchers,
            double totalPrice);

        Voucher PrintVoucher();

        Voucher ViewDetails(Voucher voucher);

        Voucher GetVoucherById(int id);

        void AddVoucher(Voucher addVoucher);

        void DeleteVoucher(int id);

        Voucher EditVoucher(Voucher voucher);
    }
}
