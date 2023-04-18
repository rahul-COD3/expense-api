using System.ComponentModel.DataAnnotations;
using System;

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
    }
}