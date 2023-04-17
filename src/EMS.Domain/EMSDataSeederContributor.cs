using AutoFixture;
using EMS.GroupMembers;

using System;

using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;


namespace EMS
{
    public class EMSDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<GroupMember, Guid> _groupMemberRepository;
        private readonly GroupMemberManager _groupMemberManager;
        private readonly IIdentityUserRepository _userRepository;
        private readonly IdentityUserManager _userManager;
        private readonly IGuidGenerator _guidGenerator;

        public EMSDataSeederContributor(GroupMemberManager groupMemberManager, IGuidGenerator guidGenerator, IRepository<GroupMember, Guid> groupMemberRepository, IIdentityUserRepository userRepository, IdentityUserManager identityUserManager)
        {
            _groupMemberRepository = groupMemberRepository;
            _userRepository = userRepository;
            _userManager = identityUserManager;
            _guidGenerator = guidGenerator;
            _groupMemberManager = groupMemberManager;

        }

        [Obsolete]
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _groupMemberRepository.GetCountAsync() > 0)
            {
                return;
            }


            await _userManager.CreateAsync(new IdentityUser(_guidGenerator.Create(), "user1", "rahul@gmail.com"), "1q2w3E*");
            await _userManager.CreateAsync(new IdentityUser(_guidGenerator.Create(), "user2", "user2@example.com"), "P@ssw0rd");

            var user1 = await _userRepository.FindByNormalizedUserNameAsync("USER1");
            var user2 = await _userRepository.FindByNormalizedUserNameAsync("USER2");

            await _groupMemberRepository.InsertAsync(
             await _groupMemberManager.CreateAsync(
                     user1.Id,
                     GuidGenerator.Create(),
                     true,
                     new DateTime(1992, 01, 11)
                    )
                 );
            await _groupMemberRepository.InsertAsync(
            await _groupMemberManager.CreateAsync(
                    user2.Id,
                    GuidGenerator.Create(),
                    true,
                    new DateTime(1922, 03, 11)
                   )
                );
        }
    }
}
