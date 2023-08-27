namespace JacksonVeroneze.TemplateWebApi.Application.Models.User.Email;

public sealed record EmailResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }
}
