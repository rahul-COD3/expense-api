using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EMS.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<bool> CheckEmailExists(string email);
    }
}
