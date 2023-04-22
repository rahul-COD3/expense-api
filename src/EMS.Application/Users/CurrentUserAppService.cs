using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Clients;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace EMS.Users
{
    public class CurrentUserAppService : ApplicationService, ICurrentUserAppService
    {
        private readonly IIdentityUserRepository _userRepository;
        public CurrentUserAppService(IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string GetCurrentUserName()
        {
            var UserName = CurrentUser?.UserName;
            if (UserName == null)
            {
                return "You Must login first";
            }
            return UserName;
        }
    }

   
}
