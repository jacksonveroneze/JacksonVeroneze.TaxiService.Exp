using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.User;

public sealed record UserResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("birthday")]
    public DateTime? Birthday { get; init; }

    [JsonPropertyName("gender")]
    public Gender? Gender { get; init; }

    [JsonPropertyName("status")]
    public UserStatus? Status { get; init; }
}
