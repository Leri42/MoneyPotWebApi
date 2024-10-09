﻿using Domain;
using Domain.Aggregates.ApplcationUserAggregate;
using Domain.Aggregates.MoneyPotAggregate;
using Domain.Aggregates.TransactionAggregate;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IMoneyPotRepository MoneyPots {  get; }

        public IMoneyPotTransactionRepository MoneyPotTransactions { get; }

        public IApplicationUserRepository ApplicationUsers { get; }


        public UnitOfWork(ApplicationDbContext context, IMoneyPotRepository moneyPots, IMoneyPotTransactionRepository transactions, IApplicationUserRepository applicationUsers)
        {
            _context = context;
            MoneyPots = moneyPots;
            MoneyPotTransactions = transactions;
            ApplicationUsers = applicationUsers;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<long> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync();
        }
    }
}
