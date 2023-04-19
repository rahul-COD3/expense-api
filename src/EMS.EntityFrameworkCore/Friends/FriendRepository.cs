using EMS.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EMS.Friends
{
    public class FriendRepository : EfCoreRepository<EMSDbContext, Friend, Guid>, IFriendRepository
    {
        public FriendRepository(IDbContextProvider<EMSDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Implement any additional methods you need here
        
        // Example method to get a friend by UserId and FriendId
        public async Task<Friend> GetByUserIdAndFriendIdAsync(Guid userId, Guid friendId)
        {
            return await GetAsync(f => f.Id == userId && f.FriendId == friendId);
        }
    }
}
