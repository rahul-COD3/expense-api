using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Volo.Abp;

namespace EMS.Expenses
{
    
    public class ExpenseAlreadyExistsException : BusinessException
    {
        public ExpenseAlreadyExistsException(string expensetitle) : base(EMSDomainErrorCodes.ExpenseAlreadyExists)
        {
            WithData("expensetitle", expensetitle);
        }
        

     
    }
}