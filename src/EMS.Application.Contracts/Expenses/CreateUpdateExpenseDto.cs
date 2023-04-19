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

        public string ExpenseTitle { get; set; }
        

        public string ExpenseDescription { get; set; }
       

        public decimal ExpenseAmount { get; set; }

        
        public SplitAsType SplitType { get; set; }
        
        public CurrencyType CurrencyType { get; set; }
       
        public bool IsSettled { get; set; }
    }
}
