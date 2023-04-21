using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EMS.GroupMembers;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;

namespace EMS.Groups;
[Authorize]
public class GroupAppService : EMSAppService, IGroupAppService
{
    private readonly IGroupRepository _groupRepository;
    private readonly GroupManager _groupManager;
    private readonly IGroupMemberRepository _groupMemberRepository;
    private readonly GroupMemberManager _groupMemberManager;
    private readonly ICurrentUser _currentUser;

    public GroupAppService(
        IGroupRepository groupRepository, 
        GroupManager groupManager, 
        IGroupMemberRepository groupMemberRepository, 
        GroupMemberManager groupMemberManager,
        ICurrentUser currentUser
        )  
    {
        _groupRepository = groupRepository;
        _groupManager = groupManager;
        _groupMemberRepository = groupMemberRepository;
        _groupMemberManager = groupMemberManager;
        _currentUser = currentUser;
    }
    // getting group by groupId
    public async Task<GroupDto> GetAsync(Guid id)
    {
        var group = await _groupRepository.GetAsync(id);
        var groupMembers = await _groupMemberRepository.FindByGroupIdAsync(id);
        var groupDto = ObjectMapper.Map<Group, GroupDto>(group);
        groupDto.GroupMembers = ObjectMapper.Map<List<GroupMember>, List<GroupMemberDto>>(groupMembers);

        return groupDto;
    }

    // getting list of group by userId
    public async Task<List<GroupDto>> GetGroupsBelongingToUserAsync(Guid userId)
    {
        var groups = await _groupRepository.FindGroupsByUserIdAsync(userId);
        return ObjectMapper.Map<List<Group>, List<GroupDto>>(groups);
    }
    // getting all group from the database
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
    // creating the group without user
    public async Task<GroupDto> CreateAsync(CreateGroupDto input)
    {
        var group = await _groupManager.CreateAsync(
            input.Name,
            input.About,
            input.CreatedBy,
            input.IsDeleted
        );
        var currentMember = await _groupMemberManager.CreateAsync(
            (Guid)_currentUser.Id,
            group.Id,
            false,
            DateTime.Now
        );
        await _groupRepository.InsertAsync(group);
        var groupMember = await _groupMemberRepository.InsertAsync(currentMember);

        var groupDto = ObjectMapper.Map<Group, GroupDto>(group);
        groupDto.GroupMembers = new List<GroupMemberDto> { ObjectMapper.Map<GroupMember, GroupMemberDto>(groupMember) };
        return groupDto;
    }

    // creating group with list of user whare user is optional
    public async Task<GroupDto> CreateWithMembersAsync(CreateGroupDto input)
    {
        // Create the group with the given input parameters
        var group = await _groupManager.CreateAsync(
            input.Name,
            input.About,
            input.CreatedBy,
            input.IsDeleted
        );

        // Add the newly created group to the database
        await _groupRepository.InsertAsync(group);

        // Add any additional group members provided in the input
        var groupMembers = input.GroupMembers.Select(groupMember =>
            _groupMemberManager.CreateAsync(
                groupMember.userId,
                group.Id,
                groupMember.isRemoved,
                groupMember.dateOfJoin
            )
        ).ToList();

        var newGroupMembers = await Task.WhenAll(groupMembers);
        await _groupMemberRepository.InsertManyAsync(newGroupMembers);

        var groupDto = ObjectMapper.Map<Group, GroupDto>(group);
        groupDto.GroupMembers = ObjectMapper.Map<List<GroupMember>, List<GroupMemberDto>>(newGroupMembers.ToList());

        return groupDto;
    }



    // updating the the group by perticular group id
    public async Task<GroupDto> UpdateAsync(Guid id, UpdateGroupDto input)
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
        return ObjectMapper.Map<Group, GroupDto>(group);
    }

    // setting the group as mark as delete
    public async Task<GroupDto> DeleteAsync(Guid id)
    {
        var group = await _groupRepository.GetAsync(id);
        group.IsDeleted = false;
        await _groupRepository.UpdateAsync(group);
        return ObjectMapper.Map<Group, GroupDto>(group);
    }
}
