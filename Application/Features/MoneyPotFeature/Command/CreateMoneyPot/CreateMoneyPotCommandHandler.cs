using Domain;
using Domain.Aggregates.MoneyPotAggregate;
using MediatR;

namespace Application.Features.MoneyPotFeature.Command.CreateMoneyPot
{
    public class CreateMoneyPotCommandHandler : IRequestHandler<CreateMoneyPotCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMoneyPotCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateMoneyPotCommand request, CancellationToken cancellationToken)
        {
            if (request.TargetAmount <= 0)
            {
                throw new Exception("The amount is belove Zero");
            }

            if(DateTime.Parse(request.Deadline) <= DateTime.UtcNow)
            {
                throw new Exception("Invalid Date");
            }

            var moneyPot = MoneyPot.Create(
           request.Title,
           request.Description,
           request.TargetAmount,
           request.Deadline,
           request.CreatorId);

            await _unitOfWork.MoneyPots.AddAsync(moneyPot);

            moneyPot.GenerateUniqueLink();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return moneyPot.UniqueLink;

        }
    }
}
