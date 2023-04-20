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

namespace EMS.Payments
{
    public class UserOutstandingDetailsAppService : ApplicationService, IUserOutstandingDetailsAppService
    {
        private readonly IRepository<Payment, Guid> _paymentRepository;
        private readonly ICurrentUser _currentUser;

        public UserOutstandingDetailsAppService(IRepository<Payment, Guid> paymentRepository, ICurrentUser currentUser)
        {
            _paymentRepository = paymentRepository;
            _currentUser = currentUser;
        }

        public async Task<List<PaymentDto>> GetUserAllOutstandingDetailAsync()
        {
            var userId = _currentUser.GetId();

            var payments = await _paymentRepository.GetListAsync();

            var userPayments = new List<Payment>();

            foreach (var payment in payments)
            {
                if (payment.Id == userId)
                {
                    userPayments.Add(payment);
                }
            }

            return ObjectMapper.Map<List<Payment>, List<PaymentDto>>(userPayments);
        }
    }
}
