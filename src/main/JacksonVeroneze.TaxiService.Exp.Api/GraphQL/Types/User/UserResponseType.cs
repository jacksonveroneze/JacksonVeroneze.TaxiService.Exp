using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

public class UserResponseType : ObjectType<UserResponse>
{
    protected override void Configure(
        IObjectTypeDescriptor<UserResponse> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(nameof(UserResponse));

        descriptor
            .Field(field => field.Id)
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(field => field.FullName)
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(field => field.Document)
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(field => field.Birthday)
            .Type<NonNullType<DateType>>();

        descriptor
            .Field(field => field.Gender)
            .Type<NonNullType<GenderType>>();

        descriptor
            .Field(field => field.Status)
            .Type<NonNullType<UserStatus>>();
    }
}