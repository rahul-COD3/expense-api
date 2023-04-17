using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMS.GroupMembers
{
    public class GroupMember : AuditedAggregateRoot<Guid>
    {
        public Guid userId { get; set; }
        public Guid groupId { get; set; }
        public bool isRemoved { get; set; }
        public DateTime dateOfJoin { get; set; }

        private GroupMember()
        {
        }

        internal GroupMember(Guid id, [NotNull] Guid userId, [NotNull] Guid groupId, [NotNull] bool isRemoved, [NotNull] DateTime dateOfJoin) : base(id)
        {
            this.userId = userId;
            this.groupId = groupId;
            this.isRemoved = isRemoved;
            this.dateOfJoin = dateOfJoin;
        }
    }
}
