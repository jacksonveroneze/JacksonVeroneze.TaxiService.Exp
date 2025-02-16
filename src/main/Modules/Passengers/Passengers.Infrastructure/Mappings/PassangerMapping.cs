using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class PassangerMapping : IEntityTypeConfiguration<PassangerEntity>
{
    public void Configure(EntityTypeBuilder<PassangerEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("passanger");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.Status);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.ComplexProperty(conf => conf.Name, conf =>
        {
            conf.Property(prop => prop.Value)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.Property(c => c.Birthday)
            .IsRequired();

        builder.Property(c => c.Gender)
            .IsRequired();

        builder.ComplexProperty(conf => conf.Cpf, conf =>
        {
            conf.Property(prop => prop.Value)
                .HasMaxLength(11)
                .HasColumnName("cpf")
                .IsRequired();
        });

        builder.Property(c => c.Status)
            .IsRequired();

        builder.Property(c => c.ActivedOnUtc);

        builder.Property(c => c.InactivedOnUtc);

        builder.ConfigureDefaultFieldsMapping();
    }
}