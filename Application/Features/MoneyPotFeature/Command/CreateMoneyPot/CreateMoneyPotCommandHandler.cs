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
            var moneyPot = MoneyPot.Create(
           request.Title,
           request.Description,
           request.UniqueLink,
           request.TargetAmount,
           request.Deadline,
           request.CreatorId);

            moneyPot.GenerateUniqueLink();

            await _unitOfWork.MoneyPots.AddAsync(moneyPot);
           await _unitOfWork.SaveChangesAsync(cancellationToken);

            return moneyPot.UniqueLink;

        }
    }
}
