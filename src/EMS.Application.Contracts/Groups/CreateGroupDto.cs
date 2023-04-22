using EMS.GroupMembers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMS.Groups
{
    public class CreateGroupDto
    {
        [Required]
        [RegularExpression(GroupConsts.GroupNameRegex, ErrorMessage = GroupConsts.GroupNameRegexErrorMessage)]
        public string Name { get; set; }

        public string? About { get; set; }

        public List<CreateGroupMemberDto>? GroupMembers { get; set; }
    }
}