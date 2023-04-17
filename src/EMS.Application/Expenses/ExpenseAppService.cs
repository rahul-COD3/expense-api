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
        Expense, //The Book entity
        ExpenseDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateExpenseDto>, //Used to create/update a book
    IExpenseAppService //implement the IBookAppService
    {
        public ExpenseAppService(IRepository<Expense, Guid> repository)
            : base(repository)
        {

        }
    }
}
