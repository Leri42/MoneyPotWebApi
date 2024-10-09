using Application.Features.MoneyPotFeature.Query.GetMoneyPot;
using Domain;
using MediatR;

namespace Application.Features.MoneyPotFeature.Query.GetMoneyPotLink
{
    public class GetMoneyPotLinkQueryHandler : IRequestHandler<GetMoneyPotLinkQuery, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetMoneyPotLinkQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(GetMoneyPotLinkQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.MoneyPots.MoneyPotLink(request.Id);
            return result;
        }
    }
}
