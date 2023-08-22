using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

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
            var userName = CurrentUser?.UserName;
            if (userName == null)
            {
                return "You Must login first";
            }
            return userName;
        }
        
        public bool IsUserLoggedIn()
        {
            var userName = CurrentUser?.UserName;
            if (userName == null)
            {
                return false;
            }
            return true;
        }
    }
}
