using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Payments
{
    public class PaymentReturnDto
    {
        public decimal Amount { get; set; }

        public Guid WhomeToGive { get; set; }

        public string GroupName { get; set; }

        public string Message { get; set; }
    }
}
