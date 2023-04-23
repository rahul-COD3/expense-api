using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace EMS.GroupMembers;

[Authorize]
public class GroupMemberAppService : EMSAppService, IGroupMemberAppService
{
    private readonly IGroupMemberRepository _groupMemberRepository;
    private readonly GroupMemberManager _groupMemberManager;

    public GroupMemberAppService(IGroupMemberRepository groupMemberRepository, GroupMemberManager groupMemberManager)
    {
        _groupMemberRepository = groupMemberRepository;
        _groupMemberManager = groupMemberManager;
    }

    public async Task<GroupMemberDto> GetAsync(Guid id)
    {
        var groupMember = await _groupMemberRepository.FirstOrDefaultAsync(gm => gm.Id == id);
        return ObjectMapper.Map<GroupMember, GroupMemberDto>(groupMember);
    }
    public async Task<List<GroupMemberDto>> GetGroupMembersAsync(Guid groupId)
    {
        var groupMembers = await _groupMemberRepository.FindByGroupIdAsync(groupId);
        return ObjectMapper.Map<List<GroupMember>, List<GroupMemberDto>>(groupMembers);
    }

    public async Task<PagedResultDto<GroupMemberDto>> GetListAsync(GetGroupMemberListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(GroupMemberDto.groupId);
        }

        var groupMembers = await _groupMemberRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting
        );

        var totalCount = await _groupMemberRepository.CountAsync();

        return new PagedResultDto<GroupMemberDto>(
            totalCount,
            ObjectMapper.Map<List<GroupMember>, List<GroupMemberDto>>(groupMembers)
        );
    }


    public async Task<GroupMemberDto> CreateAsync(CreateGroupMemberDto input)
    {
        var groupMember = await _groupMemberManager.CreateAsync(
            input.userId,
            input.groupId,
            false,
            DateTime.Now
        );

        await _groupMemberRepository.InsertAsync(groupMember);

        return ObjectMapper.Map<GroupMember, GroupMemberDto>(groupMember);
    }

    public async Task<GroupMemberDto> UpdateAsync(Guid id, UpdateGroupMemberDto input)
    {
        var groupMember = await _groupMemberRepository.FirstOrDefaultAsync(gm => gm.Id == id);
        if (groupMember == null)
        {
            throw new UserFriendlyException("Group member is not found with this id");
        }

        groupMember.userId = input.userId;
        groupMember.groupId = input.groupId;
        groupMember.isRemoved = input.isRemoved;
        groupMember.dateOfJoin = input.dateOfJoin;

        await _groupMemberRepository.UpdateAsync(groupMember);
        return ObjectMapper.Map<GroupMember, GroupMemberDto>(groupMember);
    }
    public async Task<GroupMemberDto> DeleteAsync(Guid id)
    {
        var groupMember = await _groupMemberRepository.FirstOrDefaultAsync(gm => gm.Id == id);
        if (groupMember == null)
        {
            throw new UserFriendlyException("Group member is not found with this group member id");
        }
        groupMember.isRemoved = true;
        await _groupMemberRepository.UpdateAsync(groupMember);
        return ObjectMapper.Map<GroupMember, GroupMemberDto>(groupMember);
    }

}
