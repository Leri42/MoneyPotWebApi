using Domain.Aggregates.ApplcationUserAggregate;
using Domain.Aggregates.TransactionAggregate;

namespace Domain.Aggregates.MoneyPotAggregate
{
    public class MoneyPot
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UniqueLink { get; set; }
        public decimal TargetAmount { get; set; }
        public string Deadline { get; set; }
        public decimal CurrentAmount { get; set; }
        public int CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
        public bool IsActive { get; set; }
        public List<MoneyPotTransaction> Transactions { get; set; }

        public static MoneyPot Create(string title, string description, string uniqueLink, decimal targetAmount, string deadline, int creatorId)
        {
            return new MoneyPot
            {
                Title = title,
                Description = description,
                UniqueLink = uniqueLink,
                TargetAmount = targetAmount,
                Deadline = deadline,
                CreatorId = creatorId,
                CurrentAmount = 0,
                IsActive = true,
                Transactions = new List<MoneyPotTransaction>()
            };
        }
    }
}
