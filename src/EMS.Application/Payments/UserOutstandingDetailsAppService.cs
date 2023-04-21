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
        public async Task<List<PaymentReturnDto>> GetPaymentInfoForCurrentUserAsync()
        {
            List<PaymentReturnDto> paymentReturns = new List<PaymentReturnDto>();
            var currentUserId = _currentUser.Id;

            var payments = await _paymentRepository.GetListAsync(p => p.OwnedBy == currentUserId);

            if (payments.Count == 0)
            {
                PaymentReturnDto paymentReturn = new PaymentReturnDto();
                paymentReturn.Message = "You owe no one";
                paymentReturns.Add(paymentReturn);
                return paymentReturns;
            }
            else
            {
                foreach (var payment in payments)
                {
                    var expense = await _expenseRepository.GetAsync(payment.ExpenseId);

                    var group = await _groupRepository.GetAsync(expense.group_id);

                    PaymentReturnDto paymentReturn = new PaymentReturnDto();
                    paymentReturn.Amount = payment.Amount;
                    paymentReturn.GroupName = group.Name;
                    paymentReturn.WhomeToGive = expense.paid_by;

                    paymentReturns.Add(paymentReturn);
                }

                return paymentReturns;
            }
        }


    }
}