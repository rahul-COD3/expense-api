using EMS.EntityFrameworkCore;
using EMS.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EMS.Payments
{
    public class EfCoreAuthorRepository
    : EfCoreRepository<EMSDbContext, Payment, Guid>,
        IPaymentRepository
    {
        public EfCoreAuthorRepository(
            IDbContextProvider<EMSDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Payment> GetByUserIdAsync(Guid userId)
        {
            return await GetAsync(f => f.Id == userId);
        }

    }
}
