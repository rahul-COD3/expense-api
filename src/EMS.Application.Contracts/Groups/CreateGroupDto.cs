using System;
using System.ComponentModel.DataAnnotations;

namespace EMS.Groups
{
    public class CreateGroupDto
    {
        [Required]  
        public string Name { get; set; }

        public string About { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}