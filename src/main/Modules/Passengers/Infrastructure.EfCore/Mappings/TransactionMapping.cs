using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Mappings.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Mappings;

[ExcludeFromCodeCoverage]
public class TransactionMapping : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("transaction");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => new { c.RideId, c.Status });

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.RideId)
            .IsRequired();

        builder.ComplexProperty(conf => conf.Ammount, conf =>
        {
            conf.Property(prop => prop!.Value)
                .IsRequired();
        });

        builder.Property(c => c.Date)
            .IsRequired();

        builder.Property(c => c.Status)
            .IsRequired();

        builder.ConfigureDefaultFieldsMapping();
    }
}