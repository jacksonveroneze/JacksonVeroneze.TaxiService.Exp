using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types;

public class UserType : ObjectType<UserResponse>
{
    protected override void Configure(
        IObjectTypeDescriptor<UserResponse> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Field(field => field.Id)
            .Type<NonNullType<UuidType>>();

        descriptor.Field(field => field.FullName)
            .Type<NonNullType<StringType>>();

        descriptor.Field(field => field.Birthday)
            .Type<NonNullType<DateType>>();

        descriptor.Field(field => field.Gender)
            .Type<NonNullType<EnumType<GenderType>>>();

        descriptor.Field(field => field.Status)
            .Type<NonNullType<EnumType<UserStatus>>>();
    }
}