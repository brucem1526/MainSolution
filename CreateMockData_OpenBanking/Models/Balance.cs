using System;

namespace CreateMockData_OpenBanking.Models
{
    public class Balance
    {
        public long AccountId { get; set; }
        public string CreditDebitIndicator { get; set; }
        public string Type { get; set; }
        public Decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
