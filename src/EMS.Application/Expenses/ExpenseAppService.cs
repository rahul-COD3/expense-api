using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace EMS.Expenses;

public class ExpenseAppService : EMSAppService, IExpenseAppService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ExpenseManager _expenseManager;

    public ExpenseAppService(
        IExpenseRepository expenseRepository,
        ExpenseManager expenseManager)
    {
        _expenseRepository = expenseRepository;
        _expenseManager = expenseManager;
    }

    //...SERVICE METHODS WILL COME HERE...

    public async Task<ExpenseDto> GetAsync(Guid id)
    {
        var expense = await _expenseRepository.GetAsync(id);
        return ObjectMapper.Map<Expense, ExpenseDto>(expense);

    }

    public async Task<PagedResultDto<ExpenseDto>> GetListAsync(GetExpenseListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Expense.expense_title);
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
                expense => expense.expense_title.Contains(input.Filter));

        return new PagedResultDto<ExpenseDto>(
            totalCount,
            ObjectMapper.Map<List<Expense>, List<ExpenseDto>>(expenses)
        );
    }

    public async Task<ExpenseDto> CreateAsync(CreateExpenseDto input)
    {
        var expense = await _expenseManager.CreateAsync(
            input.expense_title,
            input.expense_amount,
            input.expense_description
        );

        await _expenseRepository.InsertAsync(expense);

        return ObjectMapper.Map<Expense, ExpenseDto>(expense);
    }



    public async Task UpdateAsync(Guid id, UpdateExpenseDto input)
    {
        var expense = await _expenseRepository.GetAsync(id);

        if (expense.expense_title != input.expense_title)
        {
            await _expenseManager.ChangeNameAsync(expense, input.expense_title);
        }

        expense.expense_amount = input.expense_amount;
        expense.expense_description = input.expense_description;

        await _expenseRepository.UpdateAsync(expense);
    }

}

 