using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

internal class InactivateUserInputType
    : InputObjectType<InactivateUserCommand>
{
    protected override void Configure(
        IInputObjectTypeDescriptor<InactivateUserCommand> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor
            .Name(nameof(InactivateUserInputType));

        descriptor
            .Field(field => field.Id)
            .Type<NonNullType<UuidType>>();
    }
}