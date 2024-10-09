using Application.Mappings;
using MediatR;

namespace Application.Features.MoneyPotFeature.Command.CreateMoneyPot
{
    public class CreateMoneyPotCommand : IRequest<string>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UniqueLink { get; set; }
        public decimal TargetAmount { get; set; }
        public string Deadline { get; set; }
        public int CreatorId { get; set; }
    }
}
