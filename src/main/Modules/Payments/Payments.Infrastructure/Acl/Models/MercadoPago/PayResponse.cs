using System.Text.Json.Serialization;

namespace TaxiService.Modules.Payments.Infrastructure.Acl.Models.MercadoPago;

public record PayResponse
{
    [JsonPropertyName("status")] public string? Status { get; init; }

    [JsonPropertyName("sign_id")] public string? SignId { get; init; }

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; init; }
}
