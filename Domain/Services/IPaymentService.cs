using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(string cardNumber, decimal amount);
    }
}
