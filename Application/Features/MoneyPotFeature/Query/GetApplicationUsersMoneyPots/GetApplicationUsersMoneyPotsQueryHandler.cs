using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.MoneyPotFeature.Query.GetApplicationUsersMoneyPots
{
    public class GetApplicationUsersMoneyPotsQueryHandler : IRequestHandler<GetApplicationUsersMoneyPotsQuery, IEnumerable<ApplicationUsersMoneyPotsModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetApplicationUsersMoneyPotsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationUsersMoneyPotsModel>> Handle(GetApplicationUsersMoneyPotsQuery request, CancellationToken cancellationToken)
        {
            var moneyPots= await _unitOfWork.MoneyPots.ApplicationUserMoneyPots(request.ApplicationUserId);

            var moneyPotModels = new List<ApplicationUsersMoneyPotsModel>();

            foreach (var moneyPot in moneyPots)
            {
                var model = new ApplicationUsersMoneyPotsModel
                {
                    Title = moneyPot.Title,
                    Description = moneyPot.Description,
                    UniqueLink = moneyPot.UniqueLink,
                    TargetAmount = moneyPot.TargetAmount,
                    Deadline = moneyPot.Deadline,
                    CurrentAmount = moneyPot.CurrentAmount,
                    IsActive = moneyPot.IsActive
                };

                moneyPotModels.Add(model);
            }

            return moneyPotModels;

        }
    }
}
