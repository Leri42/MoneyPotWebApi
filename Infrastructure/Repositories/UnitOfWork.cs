using Domain;
using Domain.Aggregates.ApplcationUserAggregate;
using Domain.Aggregates.MoneyPotAggregate;
using Domain.Aggregates.TransactionAggregate;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context; 
        private IDbContextTransaction _transaction;
        public IMoneyPotRepository MoneyPots { get; }

        public IMoneyPotTransactionRepository MoneyPotTransactions { get; }

        public IApplicationUserRepository ApplicationUsers { get; }


        public UnitOfWork(ApplicationDbContext context, IMoneyPotRepository moneyPots, IMoneyPotTransactionRepository transactions, IApplicationUserRepository applicationUsers, IDbContextTransaction transaction)
        {
            _context = context;
            MoneyPots = moneyPots;
            MoneyPotTransactions = transactions;
            ApplicationUsers = applicationUsers;
            _transaction = transaction;
        }

        public void Dispose()
        {
            _context.Dispose();
            _transaction?.Dispose();
        }

        public async Task<long> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync();
        }


        public async Task<bool> CompleteAsync()
        {
            await _context.SaveChangesAsync();

            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
            _transaction.Dispose();
        }
    }
}
