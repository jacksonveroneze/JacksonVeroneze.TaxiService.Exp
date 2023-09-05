using Dapper;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Models;

[Table("user")]
public record UserModel
{
    [Dapper.Key]
    [Dapper.Required]
    [Column("id")]
    public Guid? Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("birthday")]
    public DateTime Birthday { get; set; }

    [Column("gender")]
    public int Gender { get; set; }

    [Column("cpf")]
    public string? Cpf { get; set; }

    [Column("status")]
    public int Status { get; set; }

    [Column("actived_on_utc")]
    public DateTime? ActivedOnUtc { get; set; }

    [Column("inactived_on_utc")]
    public DateTime? InactivedOnUtc { get; set; }
}
