using JacksonVeroneze.NET.Pagination.Common;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

internal class SortDirectionType
    : EnumType<SortDirection>
{
    protected override void Configure(
        IEnumTypeDescriptor<SortDirection> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(nameof(SortDirection));

        descriptor.BindValuesImplicitly();
    }
}