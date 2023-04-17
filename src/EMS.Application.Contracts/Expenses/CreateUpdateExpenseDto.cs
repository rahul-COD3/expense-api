using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMS.Expenses
{
    public class CreateUpdateExpenseDto
    {
       
        public Guid group_id { get; set; }

        public Guid paid_by { get; set; }
        [Required]
        public string expense_title { get; set; }
        [Required]
        public string expense_description { get; set; }
        [Required]
        public string split_as { get; set; }
        [Required]
        public string currency { get; set; }

        public DateTime created_at { get; set; }
        public DateTime modified_at { get; set; }
        public DateTime deleted_at { get; set; }
    }
}
