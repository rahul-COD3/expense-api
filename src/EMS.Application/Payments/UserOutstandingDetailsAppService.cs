using EMS.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using EMS.Expenses;
using EMS.Groups;

namespace EMS.Payments
{
    public class UserOutstandingDetailsAppService : ApplicationService, IUserOutstandingDetailsAppService
    {
        private readonly IRepository<Payment, Guid> _paymentRepository;
        private readonly IRepository<Expense, Guid> _expenseRepository;
        private readonly IRepository<Group, Guid> _groupRepository;
        private ICurrentUser _currentUser;

        public UserOutstandingDetailsAppService(IRepository<Payment, Guid> paymentRepository, IRepository<Expense, Guid> expenseRepository, IRepository<Group, Guid> groupRepository, ICurrentUser currentUser)
        {
            _paymentRepository = paymentRepository;
            _expenseRepository = expenseRepository;
            _groupRepository = groupRepository;
            _currentUser = currentUser;
        }

        // PaymentInfoDto
        // GroupName, Amount, PayeeId, PayeeName, 
        public async Task<PaymentReturnDto> GetPaymentInfoForCurrentUserAsync()
        {
            PaymentReturnDto paymentReturn = new PaymentReturnDto();
            var currentUserId = _currentUser.Id;

            var payment = await _paymentRepository.FirstOrDefaultAsync(p => p.OwnedBy == currentUserId);

            if (payment == null)
            {
                paymentReturn.Message = "You owes no one";
                return paymentReturn;
            }
            else
            {
                var expense = await _expenseRepository.GetAsync(payment.ExpenseId);

                var group = await _groupRepository.GetAsync(expense.group_id);

                var amount = payment.Amount;

                var groupName = group.Name;

                return (amount, groupName);
            }



        }

    }
}