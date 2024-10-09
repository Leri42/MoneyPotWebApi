
using Domain.Aggregates.ApplcationUserAggregate;
using Domain.Aggregates.MoneyPotAggregate;
using Domain.Aggregates.TransactionAggregate;

namespace Domain
{
    public interface IUnitOfWork: IDisposable
    {
        IMoneyPotRepository MoneyPots { get; }
        IMoneyPotTransactionRepository MoneyPotTransactions { get; }
        IApplicationUserRepository ApplicationUsers { get; }

        Task<long> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
