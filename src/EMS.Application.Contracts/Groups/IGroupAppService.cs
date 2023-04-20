using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMS.Groups
{
    public interface IGroupAppService : IApplicationService
    {
        Task<GroupDto> GetAsync(Guid id);

        Task<PagedResultDto<GroupDto>> GetListAsync(GetGroupListDto input);

        Task<GroupDto> CreateAsync(CreateGroupDto input);

        Task<GroupDto> UpdateAsync(Guid id, UpdateGroupDto input);

        Task<GroupDto> DeleteAsync(Guid id);
    }
}
