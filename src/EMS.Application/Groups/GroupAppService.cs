using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace EMS.Groups;
[Authorize]
public class GroupAppService : EMSAppService, IGroupAppService
{
    private readonly IGroupRepository _groupRepository;
    private readonly GroupManager _groupManager;

    public GroupAppService(IGroupRepository groupRepository, GroupManager groupManager)
    {
        _groupRepository = groupRepository;
        _groupManager = groupManager;
    }
    public async Task<GroupDto> GetAsync(Guid id)
    {
        var group = await _groupRepository.GetAsync(id);
        return ObjectMapper.Map<Group, GroupDto>(group);
    }
    public async Task<PagedResultDto<GroupDto>> GetListAsync(GetGroupListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Group.Name);
        }

        var groups = await _groupRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _groupRepository.CountAsync()
            : await _groupRepository.CountAsync(
                group => group.Name.Contains(input.Filter));

        return new PagedResultDto<GroupDto>(
            totalCount,
            ObjectMapper.Map<List<Group>, List<GroupDto>>(groups)
        );
    }
    public async Task<GroupDto> CreateAsync(CreateGroupDto input)
    {
        var group = await _groupManager.CreateAsync(
            input.Name,
            input.About,
            input.CreatedBy,
            input.IsDeleted
            );
        await _groupRepository.InsertAsync( group );
        return ObjectMapper.Map<Group,GroupDto>(group);
    }
    public async Task UpdateAsync(Guid id, UpdateGroupDto input)
    {
        var group = await _groupRepository.GetAsync( id );
        if (group.Name!=input.Name)
        {
            await _groupManager.ChangeNameAsync(group, input.Name);
        }
        group.About = input.About;
        group.CreatedBy = input.CreatedBy;
        group.IsDeleted = input.IsDeleted;
        await _groupRepository.UpdateAsync( group );
    }

    public async Task DeleteAsync(Guid id)
    {
        var group = await _groupRepository.GetAsync(id);
        group.IsDeleted = false;
        await _groupRepository.UpdateAsync(group);
    }
}
