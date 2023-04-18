using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EMS.Expenses
{
    public class ExpenseManager : DomainService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseManager(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<Expense> CreateAsync(
            Guid group_id,
            Guid paid_by,
            [NotNull] string expense_title,
            [NotNull] string expense_description,
            decimal expense_amount,
            [NotNull] string split_as,
            [NotNull] string currency)
        {
            Check.NotNullOrWhiteSpace(expense_title, nameof(expense_title));
            Check.NotNullOrWhiteSpace(expense_description, nameof(expense_description));
            Check.NotNullOrWhiteSpace(split_as, nameof(split_as));
            Check.NotNullOrWhiteSpace(currency, nameof(currency));

            return new Expense(
                GuidGenerator.Create(),
                group_id,
                paid_by,
                expense_title,
                expense_description,
                expense_amount,
                split_as,
                currency
            );
        }

        public async Task UpdateAsync(
            [NotNull] Expense expense,
            Guid group_id,
            Guid paid_by,
            [NotNull] string expense_title,
            [NotNull] string expense_description,
            decimal expense_amount,
            [NotNull] string split_as,
            [NotNull] string currency)
        {
            Check.NotNull(expense, nameof(expense));
            Check.NotNullOrWhiteSpace(expense_title, nameof(expense_title));
            Check.NotNullOrWhiteSpace(expense_description, nameof(expense_description));
            Check.NotNullOrWhiteSpace(split_as, nameof(split_as));
            Check.NotNullOrWhiteSpace(currency, nameof(currency));

            expense.Update(
                group_id,
                paid_by,
                expense_title,
                expense_description,
                expense_amount,
                split_as,
                currency
            );
        }
    }
}
