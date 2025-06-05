using System.Text.Json.Serialization;

namespace TaxiService.Modules.Payment.Infrastructure.Acl.Models.MercadoPago;

public record PayRequest
{
    [JsonPropertyName("file_name")] 
    public string? FileName { get; init; }

    [JsonPropertyName("file_content")] 
    public string? FileContent { get; init; }

    [JsonPropertyName("signature_level")] 
    public string? SignatureLevel { get; init; }
}
