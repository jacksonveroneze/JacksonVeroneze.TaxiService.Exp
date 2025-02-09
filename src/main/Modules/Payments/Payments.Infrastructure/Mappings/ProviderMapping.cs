using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Payments.Domain.Entities;

namespace TaxiService.Modules.Payments.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class ProviderMapping : IEntityTypeConfiguration<ProviderEntity>
{
    public void Configure(EntityTypeBuilder<ProviderEntity> builder)
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

        builder.ConfigureDefaultFieldsMapping();
    }
}