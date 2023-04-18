using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EMS.Expenses
{
    public interface IExpenseRepository : IRepository<Expense, Guid>
    {
        Task<Expense> FindByTitleAsync(string title);

        Task<List<Expense>> GetExpensesAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );

        Task<List<Expense>> GetExpensesByGroupIdAsync(Guid groupId);

        Task<List<Expense>> GetExpensesByPaidByAsync(Guid paidById);
    }
}
