using System.ComponentModel.DataAnnotations;
using System;
using EMS.GroupMembers;
using System.Collections.Generic;

namespace EMS.Groups
{
    public class UpdateGroupDto
    {
        [Required]
        public string Name { get; set; }

        public string About { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public List<GroupMemberDto>? GroupMembers { get; set; }
    }
}