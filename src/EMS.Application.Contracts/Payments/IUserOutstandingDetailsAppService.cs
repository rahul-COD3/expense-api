using EMS.Friends;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EMS.Payments
{
    public interface IUserOutstandingDetailsAppService : IApplicationService
    {
        Task<List<PaymentReturnDto>> GetPaymentInfoForCurrentUserAsync();
        Task<List<PaymentYouGetDto>> GetWhoWillGiveToCurrentUserAsync();
    }
}
