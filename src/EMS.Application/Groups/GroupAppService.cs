using EMS.GroupMembers;
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
    private readonly IGroupMemberRepository _groupMemberRepository;
    private readonly GroupMemberManager _groupMemberManager;

    public GroupAppService(IGroupRepository groupRepository, GroupManager groupManager, IGroupMemberRepository groupMemberRepository, GroupMemberManager groupMemberManager)
    {
        _groupRepository = groupRepository;
        _groupManager = groupManager;
        _groupMemberRepository = groupMemberRepository;
        _groupMemberManager = groupMemberManager;
    }
    public async Task<GroupDto> GetAsync(Guid id)
    {
        var group = await _groupRepository.GetAsync(id);
        return ObjectMapper.Map<Group, GroupDto>(group);
    }
    public async Task<List<GroupDto>> FindGroupByUserIdAsync(Guid userId)
    {
        var groups = await _groupRepository.FindGroupsByUserIdAsync(userId);
        return ObjectMapper.Map<List<Group>, List<GroupDto>>(groups);
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
    public async Task<GroupDto> CreateListAsync(CreateGroupDto input)
    {
        var group = await _groupManager.CreateAsync(
               input.Name,
               input.About,
               input.CreatedBy,
               input.IsDeleted
           );
        input.GroupMembers.ForEach(async groupMember =>
            {
                await _groupMemberRepository.InsertAsync(await _groupMemberManager.CreateAsync(
                   groupMember.userId,
                   group.Id,
                   groupMember.isRemoved,
                   groupMember.dateOfJoin
               ));
            });
        await _groupRepository.InsertAsync(group);
        return ObjectMapper.Map<Group, GroupDto>(group);
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
