using Volo.Abp.Application.Dtos;

namespace EMS.Groups
{
    public class GetGroupListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}