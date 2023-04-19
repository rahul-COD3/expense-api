using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using EMS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;


namespace EMS.GroupMembers
{
    public class EfCoreGroupMemberRepository : EfCoreRepository<EMSDbContext, GroupMember, Guid>,
        IGroupMemberRepository
    {
        public EfCoreGroupMemberRepository(IDbContextProvider<EMSDbContext> dbContextProvider): base(dbContextProvider)
        {
        }


        public async Task<List<GroupMember>> FindByGroupIdAsync(Guid groupId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(groupMember => groupMember.groupId == groupId).ToListAsync();
        }

        public async Task<List<GroupMember>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
