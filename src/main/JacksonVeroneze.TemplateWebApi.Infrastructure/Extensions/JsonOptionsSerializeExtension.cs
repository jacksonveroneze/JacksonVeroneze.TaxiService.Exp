using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class JsonOptionsSerializeExtension
{
    public static IServiceCollection AddJsonOptionsSerialize(
        this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition =
                JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.WriteIndented = false;
            options.SerializerOptions.PropertyNameCaseInsensitive = false;
            options.SerializerOptions.Converters
                .Add(new JsonStringEnumConverter());
        });

        return services;
    }

    public static IMvcBuilder AddJsonOptionsSerialize(
        this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition =
                JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.WriteIndented = false;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            options.JsonSerializerOptions.Converters
                .Add(new JsonStringEnumConverter());
        });

        return builder;
    }
}