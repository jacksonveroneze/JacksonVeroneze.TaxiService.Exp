namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Identity;

public record UserIdentity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
}