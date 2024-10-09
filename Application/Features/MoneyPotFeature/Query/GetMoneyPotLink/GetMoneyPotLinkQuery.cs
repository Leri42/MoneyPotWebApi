using Application.Mappings;
using MediatR;

namespace Application.Features.MoneyPotFeature.Query.GetMoneyPot
{
    public class GetMoneyPotLinkQuery : IRequest<string>
    {
        public long Id { get; set; }
    }
}
