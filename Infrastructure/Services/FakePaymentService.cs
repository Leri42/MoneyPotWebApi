using Domain.Services;

namespace Infrastructure.Services
{
    public class FakePaymentService : IPaymentService
    {
        public async Task<bool> ProcessPayment(string cardNumber, decimal amount)
        {
            await Task.Delay(200); // Delay to simulate processing
            return true; // Always returns success
        }
    }
}
