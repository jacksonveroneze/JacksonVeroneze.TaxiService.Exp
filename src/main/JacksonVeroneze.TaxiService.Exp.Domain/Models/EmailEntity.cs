namespace JacksonVeroneze.TaxiService.Exp.Domain.Models;

public class EmailModel
{
    public Guid? Id { get; set; }

    public string? Email { get; set; }

    public Guid? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int Version { get; set; }

    public Guid TenantId { get; set; }
}