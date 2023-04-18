using System;

namespace EMS.Expenses
{
    public class UpdateExpenseDto
    {
        public Guid Id { get; set; }
        public Guid group_id { get; set; }

        public Guid paid_by { get; set; }
        public string expense_title { get; set; }
        public string expense_description { get; set; }
        public decimal expense_amount { get; set; }

        public string split_as { get; set; }

        public string currency { get; set; }
    }
}