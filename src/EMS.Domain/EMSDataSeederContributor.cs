using EMS.GroupMembers;
using EMS.Groups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace EMS
{
    public class EMSDataSeederContributor
    : IDataSeedContributor, ITransientDependency
    {
        private readonly IGroupRepository _groupRepository;
        private readonly GroupManager _groupManager;
        private readonly IGroupMemberRepository _groupMemberRepository;
        private readonly GroupMemberManager _groupMemberManager;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IdentityUserManager _userManager;
        private readonly IIdentityUserRepository _userRepository;

        public EMSDataSeederContributor(
            IGroupRepository groupRepository,
            GroupManager groupManager,
            IGroupMemberRepository groupMemberRepository,
            GroupMemberManager groupMemberManager,
            IGuidGenerator guidGenerator,
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository
            )
        {
            _groupRepository = groupRepository;
            _groupManager = groupManager;
            _groupMemberRepository = groupMemberRepository;
            _groupMemberManager = groupMemberManager;
            _guidGenerator = guidGenerator;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _groupRepository.GetCountAsync()<= 0)
            {
                var rahul = new IdentityUser(_guidGenerator.Create(), "rahul", "rahul@gmail.com");
                var adi = new IdentityUser(_guidGenerator.Create(), "adi", "adi@gmail.com");

                await _userManager.CreateAsync(rahul , "1q2w3E*");
                await _userManager.CreateAsync(adi , "1q2w3E*");

                var group = await _groupManager.CreateAsync(
                    "Dinner",
                    "Dinner expense",
                    rahul.Id,
                    false
                );
                await _groupRepository.InsertAsync(group);

                // Create a task for adding the current user as a member
                var groupMembers = new List<Task<GroupMember>>();

                var rahulM = _groupMemberManager.CreateAsync(rahul.Id, group.Id, false, DateTime.Now);
                var adiM = _groupMemberManager.CreateAsync(adi.Id, group.Id, false, DateTime.Now);

                groupMembers.Add(rahulM);
                groupMembers.Add(adiM);

                var newGroupMembers = await Task.WhenAll(groupMembers);
                await _groupMemberRepository.InsertManyAsync(newGroupMembers);
            }
        }
    }
}
