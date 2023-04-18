using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMS.Payments
{
    public class Payment: AuditedAggregateRoot<Guid>
    {
        public Guid OwnedBy { get; set; }

        public Guid ExpenseId { get; set; }
        public decimal Amount { get; set; }

        public bool IsSettled { get; set; }

    }
}
