using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Enums;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

internal class User : ObjectType<UserResponse>
{
    private const string ItemName = "User";

    protected override void Configure(
        IObjectTypeDescriptor<UserResponse> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(ItemName);

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