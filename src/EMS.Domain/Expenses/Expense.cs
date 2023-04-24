using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMS.Expenses
{
    public class Expense : FullAuditedAggregateRoot<Guid>
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
