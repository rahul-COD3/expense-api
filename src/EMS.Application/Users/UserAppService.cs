using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Domain.Entities;


namespace EMS.Users
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IIdentityUserRepository _userRepository;

        public UserAppService(IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var users = await _userRepository.GetListAsync();
            return users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }



    }
}
