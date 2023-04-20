using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMS.Payments
{
    public interface IPaymentAppService :
    ICrudAppService< 
        PaymentDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdatePaymentDto>
    {

    }
}
