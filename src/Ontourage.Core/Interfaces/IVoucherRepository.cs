using Ontourage.Core.Entities;
using System.Collections.Generic;

namespace Ontourage.Core.Interfaces
{
    public interface IVoucherRepository
    {
        List<VoucherAggregate> GetAllVouchers();

        void BuyVoucher(BuyVoucherModel buyVoucher);

        VoucherAggregate GetVoucherById(int id);

        void AddVoucher(Voucher addVoucher);

        void DeleteVoucher(int id);

        void EditVoucher(Voucher voucher);

        List<VoucherAggregate> GetLowCostVouchers();
    }
}
