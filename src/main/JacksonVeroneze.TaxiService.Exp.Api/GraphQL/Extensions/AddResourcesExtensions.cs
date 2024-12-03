using HotChocolate.Execution.Configuration;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Directives;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Mutations;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Queries;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Extensions;

internal static class AddResourcesExtensions
{
    internal static IRequestExecutorBuilder AddResources(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddQueryType<QueryType>()
            .AddMutationType<MutationType>()
            .AddTypes()
            .AddDirectives();

        return builder;
    }

    private static IRequestExecutorBuilder AddTypes(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddType<UserResponseType>()
            .AddType<GetUserByIdInputType>()
            .AddType<GetUserPagedInputType>()
            .AddType<CreateUserInputType>()
            .AddType<ActivateUserInputType>()
            .AddType<GenderType>()
            .AddType<UserStatusType>();

        return builder;
    }

    private static IRequestExecutorBuilder AddDirectives(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddDirectiveType<FormatCpfDirectiveType>()
            .AddDirectiveType<LowerDirectiveType>()
            .AddDirectiveType<UpperDirectiveType>();

        return builder;
    }
}