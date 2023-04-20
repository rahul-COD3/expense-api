using Volo.Abp.Application.Dtos;

namespace EMS.Expenses
{
    public class GetExpenseListDto : PagedAndSortedResultRequestDto
    {
        public string ?Filter { get; set; }
    }
}
