using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Enums;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Inputs;

internal class CreateUserInput
    : InputObjectType<CreateUserCommand>
{
    private const string ItemName = "CreateUserInput";

    protected override void Configure(
        IInputObjectTypeDescriptor<CreateUserCommand> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(ItemName);

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
            .Field(field => field.Document)
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(field => field.Email)
            .Type<NonNullType<StringType>>();
    }
}