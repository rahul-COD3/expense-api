using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace EMS.Expenses
{
    public class ExpenseAppService : EMSAppService, IExpenseAppService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseManger _expenseManager;

        public ExpenseAppService(
            IExpenseRepository expenseRepository,
            ExpenseManger expenseManager)
        {
            _expenseRepository = expenseRepository;
            _expenseManager = expenseManager;
        }

        public async Task<ExpenseDto> GetAsync(Guid id)
        {
            var expense = await _expenseRepository.GetAsync(id);
            return ObjectMapper.Map<Expense, ExpenseDto>(expense);
        }



        public async Task<PagedResultDto<ExpenseDto>> GetListAsync(GetExpenseListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Expense.ExpenseTitle);
            }

            var expenses = await _expenseRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _expenseRepository.CountAsync()
                : await _expenseRepository.CountAsync(
                    expense => expense.ExpenseTitle.Contains(input.Filter));

            return new PagedResultDto<ExpenseDto>(
                totalCount,
                ObjectMapper.Map<List<Expense>, List<ExpenseDto>>(expenses)
            );
        }





        public async Task<ExpenseDto> CreateAsync(CreateExpenseDto input)
        {
            var expense = await _expenseManager.CreateAsync(
              input.GroupId,
              input.PaidBy,
              input.ExpenseTitle,
              input.ExpenseDescription,
              input.ExpenseAmount,
              input.SplitType,
              input.CurrencyType,
              input.IsSettled
            );

            await _expenseRepository.InsertAsync(expense);

            return ObjectMapper.Map<Expense, ExpenseDto>(expense);
        }




        public async Task UpdateAsync(Guid id, UpdateExpenseDto input)
        {
            var expense = await _expenseRepository.GetAsync(id);

            if (expense.ExpenseTitle != input.ExpenseTitle)
            {
                await _expenseManager.ChangeNameAsync(expense, input.ExpenseTitle);
            }

            expense.GroupId = input.GroupId;
            expense.PaidBy = input.PaidBy;
            expense.ExpenseTitle = input.ExpenseTitle;
            expense.ExpenseDescription = input.ExpenseDescription;
            expense.ExpenseAmount = input.ExpenseAmount;
            expense.SplitType = input.SplitType;
            expense.CurrencyType = input.CurrencyType;
            expense.IsSettled = input.IsSettled;




            await _expenseRepository.UpdateAsync(expense);
        }


        public async Task DeleteAsync(Guid id)
        {
            await _expenseRepository.DeleteAsync(id);
        }

  
    }
}
