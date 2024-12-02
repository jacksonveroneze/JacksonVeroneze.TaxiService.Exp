using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

public class CreateUserCommandType : ObjectType<CreateUserCommand>
{
    protected override void Configure(
        IObjectTypeDescriptor<CreateUserCommand> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor
            .Name(nameof(CreateUserCommand));

        descriptor
            .Field(field => field.FullName)
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(field => field.Birthday)
            .Type<NonNullType<DateType>>();

        descriptor
            .Field(field => field.Gender)
            .Type<NonNullType<GenderType>>();

        descriptor
            .Field(field => field.Email)
            .Type<NonNullType<StringType>>();
    }
}