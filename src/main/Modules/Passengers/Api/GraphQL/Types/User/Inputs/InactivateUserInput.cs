using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Inputs;

internal class InactivateUserInput
    : InputObjectType<InactivateUserCommand>
{
    private const string ItemName = "InactivateUserInput";

    protected override void Configure(
        IInputObjectTypeDescriptor<InactivateUserCommand> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(ItemName);

        descriptor
            .Field(field => field.Id)
            .Type<NonNullType<UuidType>>();
    }
}