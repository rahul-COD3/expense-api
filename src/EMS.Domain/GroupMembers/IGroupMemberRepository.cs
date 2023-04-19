using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EMS.GroupMembers;

public interface IGroupMemberRepository : IRepository<GroupMember, Guid>
{
    Task<List<GroupMember>> FindByGroupIdAsync(Guid groupId);
    Task<List<GroupMember>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}
