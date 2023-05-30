namespace JacksonVeroneze.TemplateWebApi.Domain.Entities;

public record City
{
    public int Id { get; init; }

    public string? Name { get; init; }
}