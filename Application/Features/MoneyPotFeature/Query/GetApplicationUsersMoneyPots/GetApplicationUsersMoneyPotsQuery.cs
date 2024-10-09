using Application.Mappings;
using MediatR;

namespace Application.Features.MoneyPotFeature.Query.GetApplicationUsersMoneyPots
{
    public class GetApplicationUsersMoneyPotsQuery : MapFrom<GetApplicationUsersMoneyPotsModel>, IRequest<IEnumerable<ApplicationUsersMoneyPotsModel>>
    {
        public int ApplicationUserId { get; set; }
    }
}
