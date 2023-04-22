using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace EMS.Users
{
    public interface ICurrentUserAppService : IApplicationService
    {
        string GetCurrentUserName();
    }
}
