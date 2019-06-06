using System;

namespace CreateMockData_OpenBanking.Models
{
    public class Transaction
    {
        public long AccountId { get; set; }
        public string Status { get; set; }
        public string CreditDebitIndicator { get; set; }
        public DateTime BookingDateTime { get; set; }
        public Decimal Balance { get; set; }
        public string Merchant { get; set; }
        public string MerchantCategoryName { get; set; }
    }
}
