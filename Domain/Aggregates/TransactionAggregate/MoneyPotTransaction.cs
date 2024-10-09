using Domain.Aggregates.MoneyPotAggregate;

namespace Domain.Aggregates.TransactionAggregate
{
    public class MoneyPotTransaction
    {
        public long Id { get; set; }
        public string CreateDate { get; set; }
        public decimal Amount { get; set; }
        public long ApplicationUserId { get; set; }
        public bool Status { get; set; }
        public long MoneyPotId { get; set; }
        public MoneyPot MoneyPot{ get; set; }

    }
}
