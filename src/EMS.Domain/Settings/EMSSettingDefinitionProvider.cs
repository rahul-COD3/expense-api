using Volo.Abp.Settings;

namespace EMS.Settings;

public class EMSSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(EMSSettings.MySetting1));
    }
}
