using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
namespace EMS.GroupMembers;

public class GroupMemberDto : AuditedAggregateRoot<Guid>
{
    public Guid userId { get; set; }
    public int groupId { get; set; }

    public bool isRemoved { get; set; }

    public DateTime dateOfJoin { get; set; }
}
