using System;
using Volo.Abp.Application.Dtos;

namespace EMS.GroupMembers;

public class GetGroupMemberListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}