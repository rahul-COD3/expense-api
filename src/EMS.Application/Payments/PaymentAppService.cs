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
    public class PaymentAppService :
    CrudAppService<
        Payment, 
        PaymentDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdatePaymentDto>, 
    IPaymentAppService 
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<Expense, Guid> _expenseRepository;
        private readonly IRepository<Payment, Guid> _paymentRepository;
        public PaymentAppService(IRepository<Payment, Guid> repository, ICurrentUser currentUser, IRepository<Expense, Guid> expenseRepository, IRepository<Payment, Guid> paymentRepository)
            : base(repository)
        {
            _currentUser = currentUser;
            _expenseRepository = expenseRepository;
            _paymentRepository = paymentRepository;
        }
        //public override async Task<string> UpdateAsync(Guid id, CreateUpdatePaymentDto input)
        //{
        //    var payment = await Repository.GetAsync(id);

        //    // Update the payment entity with the input values
        //    ObjectMapper.Map(input, payment);

        //    // Check if the payment has been settled
        //    if (payment.IsSettled)
        //    {
        //        // If it has, return the string "Payment Settled" in the API response
        //        return "Payment Settled";
        //    }

        //    await Repository.UpdateAsync(payment);

        //    // If the payment has not been settled, return an empty string in the API response
        //    return "";
        //}

        [HttpGet]
        [Route("api/payments/list")]
        [Authorize]
        public async Task<List<PaymentDto>> GetSettledPaymentsofCurrentUserAsync() // returns the list of payment for the current user
        {
            var check1 = await _paymentRepository.FirstOrDefaultAsync();
            var check2 = await _expenseRepository.GetAsync(check1.ExpenseId);


            var payments = await Repository.GetListAsync(p => p.IsSettled && p.OwnedBy == CurrentUser.Id || check2.paid_by == CurrentUser.Id);
            
            if(payments.Count == 0)
            {
                throw new UserFriendlyException("No Transaction to Show");
            }

            return ObjectMapper.Map<List<Payment>, List<PaymentDto>>(payments);
        }

        
    }
}
