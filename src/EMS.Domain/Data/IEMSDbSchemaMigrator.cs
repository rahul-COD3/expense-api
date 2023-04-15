using System.Threading.Tasks;

namespace EMS.Data;

public interface IEMSDbSchemaMigrator
{
    Task MigrateAsync();
}
