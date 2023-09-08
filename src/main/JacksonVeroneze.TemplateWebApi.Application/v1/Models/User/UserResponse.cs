using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;

public sealed record UserResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("birthday")]
    public DateOnly? Birthday { get; init; }

    [JsonPropertyName("gender")]
    public Gender? Gender { get; init; }

    [JsonPropertyName("status")]
    public UserStatus? Status { get; init; }
}
