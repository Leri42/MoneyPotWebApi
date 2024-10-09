using Application.Mappings;
using MediatR;

namespace Application.Features.MoneyPotTransactionFeature.Command.CreateMoneyPotTransaction
{
    public class CreateMoneyPotTransactionCommand : IRequest<bool>
    {
        public string FullName { get; set; }
        public decimal Amount { get; set; }
        public string UniqueLink { get; set; }
        public string CardNumber { get; set; }
    }
}
