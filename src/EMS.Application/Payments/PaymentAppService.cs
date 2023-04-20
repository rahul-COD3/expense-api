using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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
        public PaymentAppService(IRepository<Payment, Guid> repository)
            : base(repository)
        {

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

    }
}
