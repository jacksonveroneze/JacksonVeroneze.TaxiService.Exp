using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Payments.Domain.Entities;

namespace TaxiService.Modules.Payments.Infrastructure.EfCore.Mappings;

[ExcludeFromCodeCoverage]
public class PaymentMapping : IEntityTypeConfiguration<PaymentEntity>
{
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("payment");

        // Keys
        builder.HasKey(c => c.Id);

        builder.ConfigureDefaultFieldsMapping();
    }
}