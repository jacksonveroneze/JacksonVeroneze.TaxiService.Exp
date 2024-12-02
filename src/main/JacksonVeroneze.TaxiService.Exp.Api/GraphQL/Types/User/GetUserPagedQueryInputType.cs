using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

public class GetUserPagedQueryInputType : InputObjectType<GetUserPagedQuery>
{
    protected override void Configure(
        IInputObjectTypeDescriptor<GetUserPagedQuery> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(nameof(GetUserPagedQueryInputType));

        // descriptor.OneOf();

        descriptor
            .Field(field => field.Name)
            .Type<StringType>();

        descriptor
            .Field(field => field.Status)
            .Type<UserStatus>();
    }
}