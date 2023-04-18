using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EMS.Groups
{
    public class GroupDto:  EntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }
        public string About { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}