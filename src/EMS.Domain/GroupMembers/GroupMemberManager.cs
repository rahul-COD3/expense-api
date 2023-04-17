using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace EMS.GroupMembers;

public class GroupMemberManager : DomainService
{
    private readonly IGroupMemberRepository _groupMemberRepository;

    public GroupMemberManager(IGroupMemberRepository groupMemberRepository)
    {
        _groupMemberRepository = groupMemberRepository;
    }
    public async Task<GroupMember> CreateAsync([NotNull] Guid userId, [NotNull] Guid groupId, [NotNull] bool isRemoved, [NotNull] DateTime dateOfJoin)
    {
        return new GroupMember(
            GuidGenerator.Create(),
                userId: userId,
                groupId: groupId,
                isRemoved: isRemoved,
                dateOfJoin: dateOfJoin
            );
    }
}
