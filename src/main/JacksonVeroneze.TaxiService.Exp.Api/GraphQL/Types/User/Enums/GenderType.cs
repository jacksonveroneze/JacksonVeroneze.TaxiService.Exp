using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Enums;

internal class GenderType
    : EnumType<Gender>
{
    private const string ItemName = "GenderType";

    protected override void Configure(
        IEnumTypeDescriptor<Gender> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(ItemName);

        descriptor.BindValuesImplicitly();
    }
}