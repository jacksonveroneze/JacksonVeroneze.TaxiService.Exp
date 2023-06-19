using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MiniProfilerExtension
{
    public static IServiceCollection AddMiniProfiler(
        this IServiceCollection services)
    {
        services.AddMiniProfiler(options =>
        {
            options.RouteBasePath = "/profiler";

            options.IgnoredPaths.Add("/css");
            options.IgnoredPaths.Add("/js");
            options.IgnoredPaths.Add("/index.html");
        });

        return services;
    }
}
