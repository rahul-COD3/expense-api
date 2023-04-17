using System;
using System.ComponentModel.DataAnnotations;

namespace EMS.GroupMembers;

public class CreateGroupMemberDto
{
    [Required]
    public Guid userId { get; set; }
    [Required]
    public Guid groupId { get; set; }

    [Required]
    public bool isRemoved { get; set; } = false;

    [Required]
    public DateTime dateOfJoin { get; set; } = DateTime.Now;
}