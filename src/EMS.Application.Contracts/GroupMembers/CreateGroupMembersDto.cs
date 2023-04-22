using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMS.GroupMembers
{
    public class CreateGroupMembersDto
    {
        [Required]
        public Guid userId { get; set; }
    }
}
