using Abp.Domain.Entities.Auditing;
using System;

namespace EMS.GroupMembers;

public class GroupMemberDto : AuditedAggregateRoot<Guid>
{
    public Guid userId { get; set; }
    public Guid groupId { get; set; }

    public bool isRemoved { get; set; }

    public DateTime dateOfJoin { get; set; }
}
