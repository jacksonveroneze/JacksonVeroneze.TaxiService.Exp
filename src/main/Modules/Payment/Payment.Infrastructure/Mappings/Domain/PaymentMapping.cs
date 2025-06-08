using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;

namespace TaxiService.Modules.Payment.Infrastructure.Mappings.Domain;

[ExcludeFromCodeCoverage]
public class PaymentMapping : IEntityTypeConfiguration<Payment.Domain.Aggregates.Payment>
{
    public void Configure(EntityTypeBuilder<Payment.Domain.Aggregates.Payment> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("payment");

        builder.ConfigureEntityDefaultFieldsMapping();
    }
}
