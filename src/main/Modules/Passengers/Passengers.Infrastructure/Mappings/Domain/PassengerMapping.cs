using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Passengers.Domain.Aggregates;

namespace TaxiService.Modules.Passengers.Infrastructure.Mappings.Domain;

[ExcludeFromCodeCoverage]
public class PassengerMapping : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("passenger");

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

        builder.ConfigureEntityDefaultFieldsMapping();
    }
}
