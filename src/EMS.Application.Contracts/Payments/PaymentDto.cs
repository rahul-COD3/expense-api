using EMS.Expenses;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.Payments
{
    public class PaymentDto : AuditedEntityDto<Guid>
    {
        public Guid OwnedBy { get; set; }

        public Guid ExpenseId { get; set; }
        public decimal Amount { get; set; }

        public bool IsSettled { get; set; }

        public ExpenseDto? expenseDto { get; set; }
    }
}
