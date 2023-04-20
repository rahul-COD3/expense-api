using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
namespace EMS.Expenses
{
    public class Expense : AuditedAggregateRoot<Guid>
    {

        public Guid GroupId { get; set; }
        public Guid PaidBy { get; set; }
        public string ExpenseTitle { get;  set; }
        public string ExpenseDescription { get; set; }
        public decimal ExpenseAmount { get; set; }
        public SplitAsType SplitType { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public bool IsSettled { get; set; }

        private Expense()
        {
           
        }

        internal Expense(
            Guid id,
            [NotNull] Guid groupid,
            [NotNull] Guid paidby,
            [NotNull] string expensetitle,
            [NotNull] string expensedescription,
            [NotNull] decimal expenseamount,
            [NotNull] SplitAsType splittype,
            [NotNull] CurrencyType currencytype,
            [CanBeNull] bool issettled)

            : base(id)
        {
            GroupId = groupid;
            PaidBy = paidby;
            ExpenseTitle = expensetitle;
            ExpenseDescription = expensedescription;
            ExpenseAmount = expenseamount;
            SplitType = splittype;
            CurrencyType = currencytype;
            IsSettled = issettled;

        }

        internal Expense ChangeName([NotNull] string expensetitle)
        {
            ExpenseTitle = expensetitle;
            return this;
        }


    }
}