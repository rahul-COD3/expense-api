using EMS.Expenses;
using EMS.SettleUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace EMS
{
    public class ExpenseAppService : ApplicationService
    {
        private readonly IRepository<Expense, Guid> _expenseRepository;

        public ExpenseAppService(IRepository<Expense, Guid> expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<decimal> SettleExpense(SettleExpenseInput input)
        {
            var expense = await _expenseRepository.GetAsync(input.ExpenseId);

            if (expense == null)
            {
                throw new EntityNotFoundException(typeof(Expense), input.ExpenseId);
            }

            if (expense.Payer == input.Payer && expense.Payee == input.Payee)
            {
                expense.IsSettled = true;
                return expense.Amount / 2; // Payer has to pay half of the expense amount
            }
            else if (expense.Payer == input.Payee && expense.Payee == input.Payer)
            {
                expense.IsSettled = true;
                //return expense.Amount / 2; // Payer has to pay half of the expense amount
                return expense.Amount;
            }
            else
            {
                throw new BusinessException("Invalid Payer/Payee combination for the expense.");
            }

            await _expenseRepository.UpdateAsync(expense);
        }
    }


}