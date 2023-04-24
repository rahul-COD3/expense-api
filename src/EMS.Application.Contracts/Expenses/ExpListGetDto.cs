using EMS.Payments;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Expenses
{
    public class ExpListGetDto
    {
        public Guid? PaymentId { get; set; }
        public Guid ?OwnedBy { get; set; }
        public decimal? Amount { get; set; }
        public string? Message { get; set; }
      
    }
}
