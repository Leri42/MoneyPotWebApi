using Domain;
using Domain.Aggregates.MoneyPotAggregate;
using MediatR;
using static Application.Features.MoneyPotFeature.Query.GetMoneyPotTransactions.MoneyPotModel;

namespace Application.Features.MoneyPotFeature.Query.GetMoneyPotTransactions
{
    public class GetMoneyPotTransactionsQueryHandler : IRequestHandler<GetMoneyPotTransactionsQuery, MoneyPotModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMoneyPotTransactionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MoneyPotModel> Handle(GetMoneyPotTransactionsQuery request, CancellationToken cancellationToken)
        {
            var moneyPot = await _unitOfWork.MoneyPots.GetByIdAsync(request.Id);

            if (moneyPot == null)
            {
                return null; // Handle not found case appropriately
            }
            var result = new MoneyPotModel
            {
                Title = moneyPot.Title,
                Description = moneyPot.Description,
                UniqueLink = moneyPot.UniqueLink,
                TargetAmount = moneyPot.TargetAmount,
                Deadline = moneyPot.Deadline,
                CurrentAmount = moneyPot.CurrentAmount,
                IsActive = moneyPot.IsActive,
                Transactions = moneyPot.Transactions.Select(t => new MoneyPotTransactionModel
                {
                    Amount = t.Amount,
                    CreateDate = t.CreateDate,
                    FullName = t.FullName,
                    Status = t.Status
                }).ToList()
            }; 
            return result;
        }
    }
}
