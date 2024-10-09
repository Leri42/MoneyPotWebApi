using Domain.Aggregates.ApplcationUserAggregate;
using Domain.Aggregates.MoneyPotAggregate;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class MoneyPotRepository : GenericRepository<MoneyPot>, IMoneyPotRepository
    {
        public MoneyPotRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IQueryable<MoneyPot>> ApplicationUserMoneyPots(long ApplicationUserId)
        {
            var result = _dbSet.Where(x => x.ApplciationUserId == ApplicationUserId);
            if (result == null)
            {

            }
            return result;

        }

        public async Task<MoneyPot> MoneyPotByLink(string uniqueLink)
        {
            var moneyPot = await _dbSet.SingleAsync(c => c.UniqueLink == uniqueLink);
            if (moneyPot == null)
            {

            }

            return moneyPot;
        }

        public async Task<string> MoneyPotLink(long id)
        {
            var moneyPotUniqueLink = await _dbSet.SingleAsync(c => c.Id == id);
            if (moneyPotUniqueLink == null)
            {

            }
            return moneyPotUniqueLink.UniqueLink;
        }
    }
}
