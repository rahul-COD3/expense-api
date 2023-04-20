using EMS.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EMS.Payments
{
    public interface IPaymentRepository : IRepository<Payment, Guid>
    {
        Task<Payment> GetByUserIdAsync(Guid userId);




        // Add any additional repository methods here
    }
}
