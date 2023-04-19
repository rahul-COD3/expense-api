using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EMS.Friends
{
    public interface IFriendRepository : IRepository<Friend, Guid>
    {
        Task<Friend> GetByUserIdAndFriendIdAsync(Guid userId, Guid friendId);
       



        // Add any additional repository methods here
    }
}
