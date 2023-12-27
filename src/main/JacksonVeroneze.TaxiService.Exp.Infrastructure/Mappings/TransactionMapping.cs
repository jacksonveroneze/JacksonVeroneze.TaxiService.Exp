using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class TransactionMapping : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("transaction");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(conf => conf.Ammount, conf =>
        {
            conf.WithOwner();

            conf.Property(prop => prop.Value)
                .HasColumnName("ammount")
                .IsRequired();
        });

        builder.Property(c => c.Date)
            .IsRequired();

        builder.Property(c => c.Status)
            .IsRequired();

        builder.HasOne(p => p.Ride);

        builder.ConfigureDefaultFiledsMapping();
    }
}