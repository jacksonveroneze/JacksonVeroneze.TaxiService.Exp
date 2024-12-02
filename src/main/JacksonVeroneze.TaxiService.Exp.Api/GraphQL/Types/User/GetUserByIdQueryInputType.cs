using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

public class GetUserByIdQueryInputType : InputObjectType<GetUserByIdQuery>
{
    protected override void Configure(
        IInputObjectTypeDescriptor<GetUserByIdQuery> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(nameof(GetUserByIdQuery));

        descriptor
            .Field(field => field.Id)
            .Type<NonNullType<UuidType>>();
    }
}