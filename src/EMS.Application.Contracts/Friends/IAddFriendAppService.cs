using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EMS.Friends
{
    public interface IAddFriendAppService : IApplicationService
    {
        Task AddFriendAsync(string name, string email);
    }
}
