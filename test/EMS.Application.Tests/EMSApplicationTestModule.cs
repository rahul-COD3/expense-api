using Volo.Abp.Modularity;

namespace EMS;

[DependsOn(
    typeof(EMSApplicationModule),
    typeof(EMSDomainTestModule)
    )]
public class EMSApplicationTestModule : AbpModule
{

}
