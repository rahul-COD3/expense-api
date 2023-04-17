//using AutoFixture;
//using EMS.GroupMembers;
//using System;

//using System.Threading.Tasks;
//using Volo.Abp.Data;
//using Volo.Abp.DependencyInjection;
//using Volo.Abp.Domain.Repositories;
//using Volo.Abp.Guids;
//using Volo.Abp.Identity;
//using Volo.Abp.Users;


//namespace EMS;

//public class BookStoreDataSeederContributor
// : IDataSeedContributor, ITransientDependency
//{
//    private readonly IGroupMemberRepository _groupMemberRepository;
//    private readonly GroupMemberManager _groupMemberManager;
//    private readonly IGuidGenerator _guidGenerator;
//    private readonly IdentityUserManager _userManager;
//    private readonly IIdentityUserRepository _userRepository;

//    public BookStoreDataSeederContributor(
//        IGroupMemberRepository groupMemberRepository,
//         IGuidGenerator guidGenerator,
//         IIdentityUserRepository userRepository,
//        IdentityUserManager identityUserManager,
//        GroupMemberManager groupMemberManager)
//    {
//        _userRepository = userRepository;
//        _userManager = identityUserManager;
//        _groupMemberRepository = groupMemberRepository;
//        _groupMemberManager = groupMemberManager;
//        _guidGenerator = guidGenerator;
//    }

//    public async Task SeedAsync(DataSeedContext context)
//    {
//        if (await _groupMemberRepository.GetCountAsync() > 0)
//        {
//            return;
//        }
//        await _userManager.CreateAsync(new IdentityUser(_guidGenerator.Create(), "user1", "rahul@gmail.com"), "1q2w3E*");
//        var user1 = await _userRepository.FindByNormalizedUserNameAsync("USER1");

//        var orwell = await _groupMemberRepository.InsertAsync(
//            await _groupMemberManager.CreateAsync(
//                user1.Id,
//                     GuidGenerator.Create(),
//                     true,
//                     new DateTime(1992, 01, 11)
//                    ),
//                    autoSave: true
//                );


//    }
//}


