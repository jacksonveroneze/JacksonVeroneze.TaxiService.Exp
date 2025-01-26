using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Queries.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Queries;

internal class QueryType : ObjectType
{
    protected override void Configure(
        IObjectTypeDescriptor descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor.Name(OperationTypeNames.Query);

        descriptor.AddUserQueries();
    }
}