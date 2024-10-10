namespace Domain.Services
{
    public interface IMoneyPotService
    {
        Task CloseExpiredMoneyPots();
    }
}
