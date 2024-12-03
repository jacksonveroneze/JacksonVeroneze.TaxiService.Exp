using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

internal class GenderType
    : EnumType<Gender>
{
    protected override void Configure(
        IEnumTypeDescriptor<Gender> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(nameof(GenderType));

        descriptor.BindValuesImplicitly();
    }
}