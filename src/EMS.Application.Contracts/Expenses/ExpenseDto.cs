using System;
using Volo.Abp.Application.Dtos;

namespace EMS.Expenses
{
    public class ExpenseDto : EntityDto<Guid>
    {
        public Guid GroupId { get; set; }
        public Guid PaidBy { get; set; }
        public string ExpenseTitle { get; set; }
        public string ExpenseDescription { get; set; }
        public decimal ExpenseAmount { get; set; }
        public SplitAsType SplitType { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public bool IsSettled { get; set; }
    }
}
