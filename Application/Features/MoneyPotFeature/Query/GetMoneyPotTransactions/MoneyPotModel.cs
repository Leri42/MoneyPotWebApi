namespace Application.Features.MoneyPotFeature.Query.GetMoneyPotTransactions
{
    public class MoneyPotModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UniqueLink { get; set; }
        public decimal TargetAmount { get; set; }
        public string Deadline { get; set; }
        public decimal CurrentAmount { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<MoneyPotTransactionModel> Transactions { get; set; }
        public class MoneyPotTransactionModel
        {
            public decimal Amount { get; set; }
            public string CreateDate { get; set; }
            public string FullName { get; set; }
            public bool Status { get; set; }
        }
    }
}
