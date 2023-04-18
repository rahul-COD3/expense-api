using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMS.Expenses
{
    public interface IExpenseAppService :
        ICrudAppService< //Defines CRUD methods
        ExpenseDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateExpenseDto> //Used to create/update a book
    {

    }
    
}
