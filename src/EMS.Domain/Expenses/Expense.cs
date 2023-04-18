using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMS.Expenses
{
    public class Expense : FullAuditedAggregateRoot<Guid>
    {


        public Guid group_id { get; set; }

        public Guid paid_by { get; set; }
        public string expense_title { get; set; }
        public string expense_description { get; set; }
        public decimal expense_amount { get; set; }

        public string split_as { get; set; }

        public string currency { get; set; }

        private Expense()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Expense(
            Guid id,
            Guid group_id,
            Guid paid_by,
            [NotNull] string expense_title,
            [NotNull] string expense_description,
            decimal expense_amount,
            [NotNull] string split_as,
            [NotNull] string currency)
            : base(id)
        {
            group_id = group_id;
            paid_by = paid_by;
            SetTitle(expense_title);
            SetDescription(expense_description);
            expense_amount = expense_amount;
            split_as = split_as;
            SetCurrency(currency);
        }

        internal Expense Update(

            Guid group_id,
            Guid paid_by,
            [NotNull] string expense_title,
            [NotNull] string expense_description,
            decimal expense_amount,
            [NotNull] string split_as,
            [NotNull] string currency)
           
        {
            group_id = group_id;
            paid_by = paid_by;
            SetTitle(expense_title);
            SetDescription(expense_description);
            expense_amount = expense_amount;
            split_as = split_as;
            SetCurrency(currency);
                return this;
        }

        private void SetTitle([NotNull] string expense_title)
        {
            expense_title = Check.NotNullOrWhiteSpace(
                expense_title,
                nameof(expense_title),
                maxLength: ExpenseConsts.MaxTitleLength
            );
        }

        private void SetDescription([NotNull] string expense_description)
        {
            expense_description = Check.NotNullOrWhiteSpace(
                expense_description,
                nameof(expense_description),
                maxLength: ExpenseConsts.MaxDescriptionLength
            );
        }

        private void SetCurrency([NotNull] string currency)
        {
            currency = Check.NotNullOrWhiteSpace(
                currency,
                nameof(currency),
                maxLength: ExpenseConsts.MaxCurrencyLength
            );
        }
    }
}

