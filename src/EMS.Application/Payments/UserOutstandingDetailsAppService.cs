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

            var expenseIds = payments.Select(p => p.ExpenseId).Distinct().ToList();
            var expenses = await _expenseRepository.GetListAsync(e => expenseIds.Contains(e.Id));

            var groupIds = expenses.Select(e => e.group_id).Distinct().ToList();
            var groups = await _groupRepository.GetListAsync(g => groupIds.Contains(g.Id));

            var expenseDict = expenses.ToDictionary(e => e.Id);
            var groupDict = groups.ToDictionary(g => g.Id);

            foreach (var payment in payments)
            {
                PaymentReturnDto paymentReturn = new PaymentReturnDto();
                paymentReturn.Amount = payment.Amount;
                paymentReturn.GroupName = groupDict[expenseDict[payment.ExpenseId].group_id].Name;
                paymentReturn.WhomeToGive = expenseDict[payment.ExpenseId].paid_by;
                paymentReturns.Add(paymentReturn);
            }

            return paymentReturns;
        }


    }
}