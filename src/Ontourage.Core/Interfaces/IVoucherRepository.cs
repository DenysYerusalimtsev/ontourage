using Ontourage.Core.Entities;
using System.Collections.Generic;

namespace Ontourage.Core.Interfaces
{
    public interface IVoucherRepository
    {
        List<VoucherAggregate> GetAllVouchers();

        PaymentCheck BuyVoucher(int voucherId, int clientId, int countOfOrderedVouchers,
            double totalPrice);

        Voucher PrintVoucher();

        VoucherAggregate ViewDetails(int id);

        VoucherAggregate GetVoucherById(int id);

        void AddVoucher(Voucher addVoucher);

        void DeleteVoucher(int id);

        void EditVoucher(Voucher voucher);
    }
}
