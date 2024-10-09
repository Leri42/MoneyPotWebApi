using Domain.Aggregates.TransactionAggregate;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class MoneyPotTransactionRepository : GenericRepository<MoneyPotTransaction>, IMoneyPotTransactionRepository
    {
        public MoneyPotTransactionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
