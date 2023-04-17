using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace EMS.GroupMembers
{
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
            var groupMember = await _groupMemberRepository.GetAsync(id);
            return ObjectMapper.Map<GroupMember, GroupMemberDto>(groupMember);
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
                input.isRemoved,
                input.dateOfJoin
            );

            await _groupMemberRepository.InsertAsync(groupMember);

            return ObjectMapper.Map<GroupMember, GroupMemberDto>(groupMember);
        }

        public async Task UpdateAsync(Guid id, UpdateGroupMemberDto input)
        {
            var groupMember = await _groupMemberRepository.GetAsync(id);

            groupMember.userId = input.userId;
            groupMember.groupId = input.groupId;
            groupMember.isRemoved = input.isRemoved;
            groupMember.dateOfJoin = input.dateOfJoin;

            await _groupMemberRepository.UpdateAsync(groupMember);
        }
        public async Task DeleteAsync(Guid id)
        {
            var groupMember = await _groupMemberRepository.GetAsync(id);
            groupMember.isRemoved = false;
            await _groupMemberRepository.UpdateAsync(groupMember);
        }
    }
}
