using EMS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EMS.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EMSController : AbpControllerBase
{
    protected EMSController()
    {
        LocalizationResource = typeof(EMSResource);
    }
}
