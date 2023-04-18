using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS
{
    public class SettleExpenseInput
    {
        public Guid ExpenseId { get; set; }
        public string Payer { get; set; }
        public string Payee { get; set; }
    }
}
