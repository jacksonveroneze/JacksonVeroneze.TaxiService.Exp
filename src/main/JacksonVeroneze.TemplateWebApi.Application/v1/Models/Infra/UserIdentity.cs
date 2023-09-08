namespace JacksonVeroneze.TemplateWebApi.Application.v1.Models.Infra;

public record UserIdentity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
}
