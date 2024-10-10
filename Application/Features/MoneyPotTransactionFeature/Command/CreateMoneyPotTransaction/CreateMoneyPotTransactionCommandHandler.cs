using Application.Service;
using Domain;
using Domain.Aggregates.TransactionAggregate;
using Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.MoneyPotTransactionFeature.Command.CreateMoneyPotTransaction
{
    public class CreateMoneyPotTransactionCommandHandler : IRequestHandler<CreateMoneyPotTransactionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<MoneyPotService> _logger;

        public CreateMoneyPotTransactionCommandHandler(IUnitOfWork unitOfWork, IPaymentService paymentService, ILogger<MoneyPotService> logger)
        {
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateMoneyPotTransactionCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var paymentSuccess = await _paymentService.ProcessPayment(request.CardNumber, request.Amount);
                if (!paymentSuccess)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    _logger.LogInformation($"Unuccessfull Payment {request.CardNumber}, {request.Amount}");
                    return false; 
                }

                var moneyPot = await _unitOfWork.MoneyPots.MoneyPotByLink(request.UniqueLink);
                var transaction = MoneyPotTransaction.CreateTransaction(request.Amount, moneyPot.Id, request.FullName);
                await _unitOfWork.MoneyPotTransactions.AddAsync(transaction);

                moneyPot.CurrentAmount += request.Amount;

                if (moneyPot.CurrentAmount >= moneyPot.TargetAmount || DateTime.Parse(moneyPot.Deadline) <= DateTime.UtcNow)
                {
                    moneyPot.IsActive = false;
                    _logger.LogInformation($"Unuccessfull Transaction {moneyPot.Id} {moneyPot.IsActive}");
                }

                _logger.LogInformation($"Successfull Transaction{moneyPot.Id}, {request.Amount} {request.CardNumber}");

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex) 
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "An error occurred while makeing transaction");
                
                throw; 
            }

        }
    }
}
