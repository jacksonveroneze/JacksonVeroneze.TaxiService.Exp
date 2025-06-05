using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Payment.Domain.Aggregates;

namespace TaxiService.Modules.Payment.Infrastructure.Mappings.Domain;

[ExcludeFromCodeCoverage]
public class ProviderMapping : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("provider_config");

        // Keys
        builder.HasKey(c => c.ProviderName);

        // Properties
        builder.Property(c => c.ProviderName)
            .ValueGeneratedNever();

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.Property(c => c.Priority)
            .IsRequired();

        builder.ConfigureEntityDefaultFieldsMapping();
    }
}
