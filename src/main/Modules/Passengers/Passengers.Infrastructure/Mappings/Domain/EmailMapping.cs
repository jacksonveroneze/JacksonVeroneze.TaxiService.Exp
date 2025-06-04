using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Infrastructure.Mappings.Domain;

[ExcludeFromCodeCoverage]
public class EmailMapping : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("email");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.PassengerId);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.PassengerId)
            .IsRequired();

        builder.ComplexProperty(conf => conf.Value, conf =>
        {
            conf.Property(prop => prop.Value)
                .HasColumnName("value")
                .HasMaxLength(100);
        });

        builder.ConfigureEntityDefaultFieldsMapping();
    }
}
