namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;

public sealed record EmailResponse
{
    public Guid? Id { get; init; }

    public string? Email { get; init; }
}