using System.Linq;

namespace Domain.Aggregates.MoneyPotAggregate
{
    public interface IMoneyPotRepository:IGenericRepository<MoneyPot>
    {
        Task<string> MoneyPotLink(long id);
        Task<IQueryable<MoneyPot>> ApplicationUserMoneyPots(long ApplicationUserId);
    }
}
