using MediatR;

namespace Application.Features.MoneyPotFeature.Query.GetMoneyPotTransactions
{
    public class GetMoneyPotTransactionsQuery : IRequest<MoneyPotModel>
    {
        public int Id { get; set; }
    }
}
