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

            var result = _mapper.Map<IEnumerable<ApplicationUsersMoneyPotsModel>>(moneyPots);

            return result;

        }
    }
}
