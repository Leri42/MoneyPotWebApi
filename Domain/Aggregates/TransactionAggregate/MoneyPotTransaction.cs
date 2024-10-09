using Domain.Aggregates.MoneyPotAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Aggregates.TransactionAggregate
{
    public class MoneyPotTransaction
    {
        [Key]
        public long Id { get; set; }
        public string CreateDate { get; set; }
        public decimal Amount { get; set; }
        public int? ApplicationUserId { get; set; }
        public bool Status { get; set; }
        [ForeignKey("MoneyPot")]
        public long? MoneyPotId { get; set; }
        public virtual MoneyPot MoneyPot{ get; set; }
        public string FullName { get; set; }
        public MoneyPotTransaction()
        {
            CreateDate = DateTime.UtcNow.ToShortDateString();  // Default creation date
            Status = true;  // Default status is set to true
        }
        public MoneyPotTransaction(decimal amount, long moneyPotId, string fullName)
        {
            Amount = amount;
            MoneyPotId = moneyPotId;
            CreateDate = DateTime.Now.ToShortDateString();  // Automatically set creation date
            Status = true;  // Default status is set to true
            FullName = fullName;
        }

        public static MoneyPotTransaction CreateTransaction(decimal amount, long moneyPotId, string fullName)
        {
            return new MoneyPotTransaction(amount, moneyPotId, fullName);
        }

    }
}
