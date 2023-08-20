using Dapper;

namespace JacksonVeroneze.TemplateWebApi.Domain.Results;

[Table("bank")]
public class BankResult
{
    [Key]
    [Required]
    [Column("id")]
    public Guid? Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("status")]
    public int Status { get; set; }
}
