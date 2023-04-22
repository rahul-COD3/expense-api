using System;
using System.ComponentModel.DataAnnotations;

namespace EMS.GroupMembers;

public class CreateGroupMemberDto
{
    [Required]
    public Guid userId { get; set; }
    [Required]
    public Guid groupId { get; set; }
}