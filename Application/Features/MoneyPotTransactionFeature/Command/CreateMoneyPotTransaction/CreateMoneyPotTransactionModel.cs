namespace Application.Features.MoneyPotTransactionFeature.Command.CreateMoneyPotTransaction
{
    public class CreateMoneyPotTransactionModel
    {
        public string FullName { get; set; }
        public decimal Amount { get; set; }
        public string UniqueLink { get; set; }
    }
}
