using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Users;

namespace EMS.Friends
{
    public class Friend :AuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public bool IsDeleted { get; set; }
        

    }
}
