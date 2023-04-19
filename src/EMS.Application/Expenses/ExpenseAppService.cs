using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EMS.Expenses
{
    public class ExpenseAppService :
      CrudAppService<
          Expense, 
          ExpenseDto, 
          Guid, 
          CreateUpdateExpenseDto>, 
      IExpenseAppService 
    {
        public ExpenseAppService(IRepository<Expense, Guid> repository)
            : base(repository)
        {

        }
    }
}
