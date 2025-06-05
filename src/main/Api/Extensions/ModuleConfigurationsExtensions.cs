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
                $"appsettings.{module}.{environment.EnvironmentName}.json",
                optional: true, reloadOnChange: true);
        }

        return configurationBuilder;
    }
}
