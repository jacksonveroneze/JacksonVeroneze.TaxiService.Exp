namespace TaxiService.Api.Extensions;

internal static class ModuleConfigurationsExtensions
{
    internal static IConfigurationBuilder AddModuleConfiguration(
        this IConfigurationBuilder configurationBuilder,
        IWebHostEnvironment environment,
        string[] modules)
    {
        foreach (string module in modules)
        {
            configurationBuilder.AddJsonFile(
                $"appsettings.{module}.{environment.EnvironmentName}.json", true, true);
        }

        return configurationBuilder;
    }
}