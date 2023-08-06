namespace JacksonVeroneze.TemplateWebApi.Application.Models.Infra;

public record UserIdentity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
}
