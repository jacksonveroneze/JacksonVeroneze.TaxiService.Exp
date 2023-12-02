using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

public sealed record CreateUserCommand :
    IRequest<Result<CreateUserCommandResponse>>
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("birthday")]
    public DateOnly? Birthday { get; init; }

    [JsonPropertyName("gender")]
    public GenderType? Gender { get; init; }

    [JsonPropertyName("document")]
    public string? Document { get; init; }
}
