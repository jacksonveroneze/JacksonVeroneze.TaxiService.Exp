namespace JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;

public sealed record RideResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }
}
