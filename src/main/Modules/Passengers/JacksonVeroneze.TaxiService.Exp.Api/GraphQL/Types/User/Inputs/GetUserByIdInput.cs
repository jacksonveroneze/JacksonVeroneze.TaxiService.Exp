using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Inputs;

internal class GetUserByIdInput
    : InputObjectType<GetUserByIdQuery>
{
    private const string ItemName = "GetUserByIdInput";

    protected override void Configure(
        IInputObjectTypeDescriptor<GetUserByIdQuery> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(ItemName);

        descriptor
            .Field(field => field.Id)
            .Type<NonNullType<UuidType>>();
    }
}