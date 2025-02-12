namespace TaxiService.Api.Extensions;

internal static class ModuleConfigurationsExtensions
{
    internal static IConfigurationBuilder AddModuleConfiguration(
        this IConfigurationBuilder configurationBuilder,
        string[] modules)
    {
        foreach (string module in modules)
        {
            configurationBuilder.AddJsonFile(
                $"appsettings.{module}.Development.json", true, true);
            
            configurationBuilder.AddJsonFile(
                $"appsettings.{module}.Production.json", false, true);
        }

        return configurationBuilder;
    }
}