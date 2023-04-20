using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EMS.Expenses
{
    public class ExpenseManger : DomainService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseManger(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<Expense> CreateAsync(
            [NotNull] Guid groupid,
            [NotNull] Guid paidby,
            [NotNull] string expensetitle,
            [NotNull] string expensedescription,
            [NotNull] decimal expenseamount,
            [NotNull] SplitAsType splittype,
            [NotNull] CurrencyType currencytype,
            [NotNull] bool issettled)
        {
            Check.NotNullOrWhiteSpace(expensetitle, nameof(expensetitle));

            var existingExpense = await _expenseRepository.FindByNameAsync(expensetitle);
            if (existingExpense != null)
            {
                throw new ExpenseAlreadyExistsException(expensetitle);
            }

            return new Expense(
                Guid.Empty,
                groupid,
                paidby,
                expensetitle,
                expensedescription,
                expenseamount,
                splittype,
                currencytype,
                issettled
               
            );
        }

        public async Task ChangeNameAsync(
        [NotNull] Expense expense,
        [NotNull] string newName)
        {
            Check.NotNull(expense, nameof(expense));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingExpense = await _expenseRepository.FindByNameAsync(newName);
            if (existingExpense != null && existingExpense.Id != expense.Id)
            {
                throw new ExpenseAlreadyExistsException(newName);
            }

            expense.ChangeName(newName);
        }

       
    }
}
