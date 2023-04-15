using EMS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EMS.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EMSEntityFrameworkCoreModule),
    typeof(EMSApplicationContractsModule)
    )]
public class EMSDbMigratorModule : AbpModule
{

}
