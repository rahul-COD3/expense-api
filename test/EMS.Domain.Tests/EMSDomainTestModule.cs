using EMS.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EMS;

[DependsOn(
    typeof(EMSEntityFrameworkCoreTestModule)
    )]
public class EMSDomainTestModule : AbpModule
{

}
