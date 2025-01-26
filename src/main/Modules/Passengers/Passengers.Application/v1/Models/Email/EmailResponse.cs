namespace TaxiService.Modules.Passengers.Application.v1.Models.Email;

public sealed record EmailResponse
{
    public Guid? Id { get; init; }

    public string? Email { get; init; }
}