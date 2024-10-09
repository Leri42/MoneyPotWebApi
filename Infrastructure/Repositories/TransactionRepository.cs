using Domain.Aggregates.TransactionAggregate;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class MoneyPotTransactionRepository : GenericRepository<MoneyPotTransaction>, IMoneyPotTransactionRepository
    {
        public MoneyPotTransactionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
