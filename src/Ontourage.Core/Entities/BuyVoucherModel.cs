namespace Ontourage.Core.Entities
{
    public class BuyVoucherModel
    {
        public int ClientId { get; }

        public int VoucherId { get; }

        public int CountOfVouchers { get; }

        public double TotalPrice { get; }

        public BuyVoucherModel(int clientId, int voucherId, 
            int countOfVouchers, double totalPrice)
        {
            ClientId = clientId;
            VoucherId = voucherId;
            CountOfVouchers = countOfVouchers;
            TotalPrice = totalPrice;
        }
    }
}
