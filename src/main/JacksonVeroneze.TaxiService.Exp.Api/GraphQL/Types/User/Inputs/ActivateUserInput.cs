using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Inputs;

internal class ActivateUserInput
    : InputObjectType<ActivateUserCommand>
{
    private const string ItemName = "ActivateUserInput";

    protected override void Configure(
        IInputObjectTypeDescriptor<ActivateUserCommand> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(ItemName);

        descriptor
            .Field(field => field.Id)
            .Type<NonNullType<UuidType>>();
    }
}