using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Payments
{
    public class PaymentYouGetDto
    {
        public Guid? OwesFromYou { get; set; }
        public decimal? Amount { get; set; }
        public string? GroupName { get; set; }
        public string? Message { get; set; }
    }
}
