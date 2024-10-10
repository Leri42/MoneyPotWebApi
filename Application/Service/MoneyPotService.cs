using Domain;
using Domain.Aggregates.MoneyPotAggregate;
using Domain.Services;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.Service
{
    public class MoneyPotService : IMoneyPotService
    {
        private readonly IGenericRepository<MoneyPot> _moneyPotRepository;
        private readonly ILogger<MoneyPotService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public MoneyPotService(
            IGenericRepository<MoneyPot> moneyPotRepository,
            IUnitOfWork unitOfWork,
            ILogger<MoneyPotService> logger)
        {
            _moneyPotRepository = moneyPotRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task CloseExpiredMoneyPots()
        {
            try
            {
                var allMoneyPots = await _moneyPotRepository.GetAllAsync();
                var now = DateTime.UtcNow;

                var expiredPots = allMoneyPots.Where(mp => mp.IsActive &&
                    (DateTime.Parse(mp.Deadline) <= now || mp.CurrentAmount >= mp.TargetAmount)).ToList();

                foreach (var pot in expiredPots)
                {
                    pot.IsActive = false;


                    await _unitOfWork.MoneyPots.UpdateAsync(pot);
                    _logger.LogInformation($"Closed MoneyPot {pot.Id}: Status = {pot.IsActive}");
                }

                await _unitOfWork.CompleteAsync();
                _logger.LogInformation($"Closed {expiredPots.Count} MoneyPots");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while closing expired MoneyPots");
                throw;
            }
        }
    }
}
