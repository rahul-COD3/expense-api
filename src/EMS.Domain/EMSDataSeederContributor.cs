//using AutoFixture;
//using EMS.Expenses;
//using EMS.Groups;
//using Microsoft.VisualBasic;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Volo.Abp.Data;
//using Volo.Abp.DependencyInjection;
//using Volo.Abp.Domain.Repositories;
//using Volo.Abp.Guids;

//namespace EMS
//{
//    public class EMSDataSeederContributor
//    : IDataSeedContributor, ITransientDependency
//    {
//        private readonly IRepository<Group, Guid> _groupRepository;
//        private readonly GroupManager _groupManager;
       

//        public EMSDataSeederContributor(IRepository<Group, Guid> groupRepository, GroupManager groupManager)
//        {
//            _groupRepository = groupRepository;
//            _groupManager = groupManager;
            
//        }

//        public async Task SeedAsync(DataSeedContext context)
//        {
//            if (await _groupRepository.GetCountAsync() > 0)
//            {
//                return;
//            }
//            Guid guidobj = Guid.NewGuid();
//            Guid guidobj1 = Guid.NewGuid();
//            var user = await _groupRepository.InsertAsync(
//                   await _groupManager.CreateAsync(
//                       "Promact",
//                       "Software",
//                        guidobj,
//                       false
//                    )
//                );
//           var user1= await _groupRepository.InsertAsync(
//                   await _groupManager.CreateAsync(
//                       "Vacation",
//                       "Software Service",
//                       guidobj1,
//                       false
//                     )
//                );

//        }
//    }
//}
