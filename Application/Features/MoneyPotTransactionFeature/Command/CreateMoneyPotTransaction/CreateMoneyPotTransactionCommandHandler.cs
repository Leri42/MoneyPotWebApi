using Domain;
using Domain.Aggregates.TransactionAggregate;
using Domain.Services;
using MediatR;

namespace Application.Features.MoneyPotTransactionFeature.Command.CreateMoneyPotTransaction
{
    public class CreateMoneyPotTransactionCommandHandler : IRequestHandler<CreateMoneyPotTransactionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public CreateMoneyPotTransactionCommandHandler(IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<bool> Handle(CreateMoneyPotTransactionCommand request, CancellationToken cancellationToken)
        {
            var paymentSuccess = await _paymentService.ProcessPayment(request.CardNumber, request.Amount);

            if (!paymentSuccess)
            {
                return false;
            }


            var moneyPot = await _unitOfWork.MoneyPots.MoneyPotByLink(request.UniqueLink);



            var transaction = MoneyPotTransaction.CreateTransaction(request.Amount, moneyPot.Id, request.FullName);

            await _unitOfWork.MoneyPotTransactions.AddAsync(transaction);

            moneyPot.CurrentAmount += request.Amount;

            if (moneyPot.CurrentAmount >= moneyPot.TargetAmount || DateTime.Parse(moneyPot.Deadline) <= DateTime.UtcNow)
            {
                moneyPot.IsActive = false;
            }

            await _unitOfWork.CompleteAsync();

            return true;

        }
    }
}
