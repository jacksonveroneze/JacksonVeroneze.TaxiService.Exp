namespace TaxiService.Modules.Passenger.Application.v1.Email.Common.Models;

public sealed record EmailResponse
{
    public Guid? Id { get; init; }

    public string? Email { get; init; }
}
