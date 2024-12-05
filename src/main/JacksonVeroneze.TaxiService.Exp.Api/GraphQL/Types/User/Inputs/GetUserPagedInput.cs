using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Enums;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Inputs;

internal class GetUserPagedInput
    : InputObjectType<GetUserPagedQuery>
{
    private const string ItemName = "GetUserPagedInput";

    protected override void Configure(
        IInputObjectTypeDescriptor<GetUserPagedQuery> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(ItemName);

        descriptor
            .Field(field => field.Name)
            .Type<StringType>();

        descriptor
            .Field(field => field.Status)
            .Type<UserStatus>();

        descriptor
            .Field(field => field.Cursor)
            .Type<StringType>();

        descriptor
            .Field(field => field.OrderBy)
            .Type<StringType>();

        descriptor
            .Field(field => field.Order)
            .Type<SortDirection>();
    }
}