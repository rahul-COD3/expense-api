using EMS.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;

namespace EMS.Payments
{
    public class PaymentAppService : ApplicationService, IPaymentAppService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<Expense, Guid> _expenseRepository;
        private readonly IRepository<Payment, Guid> _paymentRepository;
        public PaymentAppService(IRepository<Payment, Guid> repository, ICurrentUser currentUser, IRepository<Expense, Guid> expenseRepository, IRepository<Payment, Guid> paymentRepository)
            : base()
        {
            _currentUser = currentUser;
            _expenseRepository = expenseRepository;
            _paymentRepository = paymentRepository;
        }

        
        public async Task<Payment> UpdatePaymentAsync(Guid Id)
        {
            var pay1 = await _paymentRepository.FirstOrDefaultAsync(p => p.Id == Id);
            if (pay1 == null)
            {
                throw new UserFriendlyException("Payment not found");
                
            }
            if (pay1.IsSettled)
            {
                throw new UserFriendlyException("Payment Already Settled");
            }
            pay1.IsSettled = true;
            return pay1;
        }


        [HttpGet]
        [Route("api/payments/list")]
        [Authorize]
        public async Task<List<PaymentDto>> GetSettledPaymentsofCurrentUserAsync() // returns the list of payment for the current user
        {
            var pay1 = await _paymentRepository.FirstOrDefaultAsync();
            var pay2 = await _expenseRepository.GetAsync(pay1.ExpenseId);


            var payments = await _paymentRepository.GetListAsync(p => p.IsSettled && p.OwnedBy == CurrentUser.Id || pay2.paidBy == CurrentUser.Id);
            
            if(payments.Count == 0)
            {
                throw new UserFriendlyException("No Transaction to Show");
            }

            return ObjectMapper.Map<List<Payment>, List<PaymentDto>>(payments);
        }

        
    }
}
