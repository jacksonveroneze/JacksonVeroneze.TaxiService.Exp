using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;

public sealed record UserResponse
{
    public Guid? Id { get; init; }

    public string? FullName { get; init; }

    public string? Document { get; init; }

    public DateOnly? Birthday { get; init; }

    public Gender? Gender { get; init; }

    public UserStatus? Status { get; init; }
}