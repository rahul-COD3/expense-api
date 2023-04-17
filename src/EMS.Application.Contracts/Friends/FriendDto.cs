using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.Friends
{
    public class FriendDto : AuditedEntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
