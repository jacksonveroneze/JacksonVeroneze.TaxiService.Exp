namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Enums;

internal class SortDirection
    : EnumType<NET.Pagination.Common.SortDirection>
{
    private const string ItemName = "SortDirection";

    protected override void Configure(
        IEnumTypeDescriptor<NET.Pagination.Common.SortDirection> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(ItemName);

        descriptor.BindValuesImplicitly();
    }
}