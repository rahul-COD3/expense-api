using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMS.Payments
{
    public class CreateUpdatePaymentDto
    {
        [Required]
        public Guid OwnedBy { get; set; }

        [Required]
        public Guid ExpenseId { get; set; }
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool IsSettled { get; set; }
    }
}
