using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Mutations.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Mutations;

internal class MutationType : ObjectType
{
    protected override void Configure(
        IObjectTypeDescriptor descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(OperationTypeNames.Mutation);

        descriptor.AddUserMutations();
    }
}