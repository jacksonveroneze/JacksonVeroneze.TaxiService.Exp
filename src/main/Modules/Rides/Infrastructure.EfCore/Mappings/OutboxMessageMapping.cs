using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Infrastructure.EfCore.Mappings.Extensions;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Mappings;

[ExcludeFromCodeCoverage]
public class OutboxMessageMapping : IEntityTypeConfiguration<OutboxMessageEntity>
{
    public void Configure(EntityTypeBuilder<OutboxMessageEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("outbox_message");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.Type);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Type)
            .IsRequired();

        builder.Property(c => c.Content)
            .IsRequired();

        builder.Property(c => c.OccurredOnUtc);

        builder.ConfigureDefaultFieldsMapping();
    }
}