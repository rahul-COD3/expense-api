using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using EMS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EMS.Groups
{
    public class EfCoreGroupRepository : EfCoreRepository<EMSDbContext, Group, Guid>,
        IGroupRepository
    {
        public EfCoreGroupRepository(IDbContextProvider<EMSDbContext> dbContextProvider) : base(dbContextProvider)
        {
           
        }
        public async Task<Group> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(group => group.Name == name);
        }
        public async Task<List<Group>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    group => group.Name.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
