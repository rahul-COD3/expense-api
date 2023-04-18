using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMS.Expenses
{
    public interface IExpenseAppService : IApplicationService
    {
        Task<ExpenseDto> GetAsync(Guid id);

        Task<PagedResultDto<ExpenseDto>> GetListAsync(GetExpenseListDto input);

        Task<ExpenseDto> CreateAsync(CreateExpenseDto input);

        Task UpdateAsync(Guid id, UpdateExpenseDto input);

        Task DeleteAsync(Guid id);



  
    }
}
