using Domain.Aggregates.ApplcationUserAggregate;
using Domain.Aggregates.TransactionAggregate;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates.MoneyPotAggregate
{
    public class MoneyPot
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UniqueLink { get; set; }
        public decimal TargetAmount { get; set; }
        public string Deadline { get; set; }
        public decimal CurrentAmount { get; set; }
        [ForeignKey("ApplicationUser")]
        public int? ApplciationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public bool IsActive { get; set; }
        public List<MoneyPotTransaction> Transactions { get; set; }

        public static MoneyPot Create(string title, string description,  decimal targetAmount, string deadline, int creatorId)
        {
            return new MoneyPot
            {
                Title = title,
                Description = description,
                TargetAmount = targetAmount,
                Deadline = deadline,
                ApplciationUserId = creatorId,
                CurrentAmount = 0,
                IsActive = true,
                Transactions = new List<MoneyPotTransaction>()
            };
        }
        public void GenerateUniqueLink()
        {

            UniqueLink = LinkGenerator.GenerateMoneyPotLink(Id);
        }


    }
    public static class LinkGenerator
    {
        public static string GenerateMoneyPotLink(long moneyPotId)
        {
            // Replace this with your actual domain or dynamically retrieve it from configuration
            string baseUrl = "https://localhost:44331";  // Assuming you're using localhost for development
            string moneyPotPath = $"/api/MoneyPot/{moneyPotId}";

            // Combine the base URL and the MoneyPot path
            return $"{baseUrl}{moneyPotPath}";
        }
    }
}
