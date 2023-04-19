using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.Expenses
{
    public class ExpenseDto : AuditedEntityDto<Guid>
    {
        public Guid groupId { get; set; }
        public Guid paidBy { get; set; }
        public string ExpenseTitle { get; set; }
        public string ExpenseDescription { get; set; }
        public decimal ExpenseAmount { get; set; }
        public SplitAsType SplitType { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public bool IsSettled { get; set; }
    }
}
