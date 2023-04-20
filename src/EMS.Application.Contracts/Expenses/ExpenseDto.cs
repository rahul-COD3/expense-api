using EMS.Groups;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.Expenses
{
    public class ExpenseDto : AuditedEntityDto<Guid>
    {
        public Guid group_id { get; set; }

        public Guid paid_by { get; set; }
        public string expense_title { get; set; }
        public string expense_description { get; set; }
        public decimal expense_amount { get; set; }

        public string split_as { get; set; }

        public string currency { get; set; }

        public DateTime created_at { get; set; }
        public DateTime modified_at { get; set; }
        public DateTime deleted_at { get; set; }
        public GroupDto? groupDto { get; set; }
    }
}
