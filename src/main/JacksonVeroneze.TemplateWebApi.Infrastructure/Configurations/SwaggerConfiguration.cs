namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class SwaggerConfiguration
{
    [Required]
    public string? ContactName { get; set; }

    [Required]
    public string? ContactEmail { get; set; }
}