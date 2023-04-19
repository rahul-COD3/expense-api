using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
namespace EMS.Expenses
{
    public class Expense : AuditedAggregateRoot<Guid>
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