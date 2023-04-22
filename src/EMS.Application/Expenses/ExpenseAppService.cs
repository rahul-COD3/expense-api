using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using EMS.GroupMembers;
using EMS.Payments;
using Volo.Abp.Guids;

namespace EMS.Expenses
{
    public class ExpenseAppService :
        CrudAppService<
        Expense, //The Book entity
        ExpenseDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateExpenseDto>, //Used to create/update a book
    IExpenseAppService //implement the IBookAppService
    {
        private readonly IRepository<GroupMember, Guid> _groupMemberRepository;
        private readonly IRepository<Payment, Guid> _paymentRepository;
        private readonly IRepository<Expense, Guid> _expenseRepository;
        
        public ExpenseAppService(IRepository<Expense, Guid> repository, IGroupMemberRepository groupMemberRepository, IRepository<Payment, Guid> paymentRepository)
            : base(repository)
        {
            _groupMemberRepository = groupMemberRepository;
            _paymentRepository = paymentRepository;


        }

        public override async Task<ExpenseDto> CreateAsync(CreateUpdateExpenseDto input)
        {
            var expense = ObjectMapper.Map<CreateUpdateExpenseDto, Expense>(input);

            // retrieve associated group members
            var groupMembers = await _groupMemberRepository.GetListAsync(gm => gm.groupId == input.group_id);

            //var groupMembers = await _groupMemberRepository.FindByGroupIdAsync(groupId);
            await Repository.InsertAsync(expense);

            // calculate the amount per group member
            var amountPerMember = expense.expense_amount / groupMembers.Count;

            var abc = groupMembers.RemoveAll(gm => gm.userId == expense.paid_by);

            // create a new payment entity for each group member
            foreach (var groupMember in groupMembers)
            {
                var payment = new Payment
                {
                    OwnedBy = groupMember.userId,
                    ExpenseId = expense.Id,
                    Amount = amountPerMember,
                    IsSettled = false
                };

                await _paymentRepository.InsertAsync(payment);
            }

           

            return MapToGetOutputDto(expense);
        }

    }

}
