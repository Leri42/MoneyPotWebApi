using Domain.Aggregates.MoneyPotAggregate;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services
{
    public class MoneyPotRepository : GenericRepository<MoneyPot>, IMoneyPotRepository
    {
        public MoneyPotRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IQueryable<MoneyPot>> ApplicationUserMoneyPots(long ApplicationUserId)
        {
            var result = _dbSet.Where(x=>x.CreatorId== ApplicationUserId);
            if (result == null)
            {

            }
            return result;

        }

        public async Task<string> MoneyPotLink(long id)
        {
            var moneyPotUniqueLink = _dbSet.FirstOrDefault(c => c.Id == id)?.UniqueLink;
            if(moneyPotUniqueLink == null)
            {

            }
            return moneyPotUniqueLink.ToString();
        }
    }
}
