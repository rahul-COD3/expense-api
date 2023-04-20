using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using EMS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EMS.Expenses
{
    public class EfCoreExpenseRepository
        : EfCoreRepository<EMSDbContext, Expense, Guid>,
         IExpenseRepository
    {
        public EfCoreExpenseRepository(
            IDbContextProvider<EMSDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public async Task<Expense> FindByNameAsync(string expensetitle)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(expense => expense.ExpenseTitle == expensetitle);
        }

        public async Task<List<Expense>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    expense => expense.ExpenseTitle.Contains(filter)
                    )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
