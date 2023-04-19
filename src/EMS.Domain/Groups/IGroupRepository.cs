using EMS.GroupMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EMS.Groups;

public interface IGroupRepository : IRepository<Group, Guid>
{
    Task<List<Group>> FindGroupsByUserIdAsync(Guid userId);
    Task<Group> FindByNameAsync(string name);
    Task<List<Group>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
}
