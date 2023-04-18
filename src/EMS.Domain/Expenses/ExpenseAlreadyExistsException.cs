using Volo.Abp;

namespace EMS.Expenses
{
    public class ExpenseAlreadyExistsException : BusinessException
    {
        public ExpenseAlreadyExistsException(string expenseTitle)
          //  : base(ExpenseDomainErrorCodes.ExpenseAlreadyExists)
        {
            WithData("expenseTitle", expenseTitle);
        }
    }
}
