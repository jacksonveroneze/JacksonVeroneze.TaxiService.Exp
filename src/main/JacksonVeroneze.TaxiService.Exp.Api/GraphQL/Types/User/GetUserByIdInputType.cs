using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

internal class GetUserByIdInputType
    : InputObjectType<GetUserByIdQuery>
{
    protected override void Configure(
        IInputObjectTypeDescriptor<GetUserByIdQuery> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(nameof(GetUserByIdInputType));

        descriptor
            .Field(field => field.Id)
            .Type<NonNullType<UuidType>>();
    }
}