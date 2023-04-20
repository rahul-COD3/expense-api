using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EMS.Expenses
{
    public interface IExpenseRepository : IRepository<Expense, Guid>
    {
        Task<Expense> FindByNameAsync(string name);
        Task<List<Expense>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
    }
}