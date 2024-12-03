using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

public class ActivateUserInputType
    : InputObjectType<ActivateUserCommand>
{
    protected override void Configure(
        IInputObjectTypeDescriptor<ActivateUserCommand> descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor
            .Name(nameof(ActivateUserInputType));

        descriptor
            .Field(field => field.Id)
            .Type<NonNullType<UuidType>>();
    }
}