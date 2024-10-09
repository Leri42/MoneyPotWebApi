
using Microsoft.AspNetCore.Identity;

namespace Domain.Aggregates.ApplcationUserAggregate
{
    public class ApplicationUser:IdentityUser
    {
        public decimal Balance { get; set; }
    }
}
