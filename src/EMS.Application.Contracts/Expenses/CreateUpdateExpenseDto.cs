using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMS.Expenses
{
    public class CreateUpdateExpenseDto
    {

        public Guid groupId { get; set; }

        public Guid paidBy { get; set; }
        public string expense_title { get; set; }
        public string expense_description { get; set; }
        public decimal expense_amount { get; set; }

        public string split_as { get; set; }

        public string currency { get; set; }

        public bool IsSettled { get; set; }

    }
}
