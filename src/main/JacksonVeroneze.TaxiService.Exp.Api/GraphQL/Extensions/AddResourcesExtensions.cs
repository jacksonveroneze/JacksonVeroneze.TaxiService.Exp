using HotChocolate.Execution.Configuration;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Directives;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Mutations;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Queries;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Enums;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Inputs;

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
            .AddType<User>()
            .AddType<GetUserByIdInput>()
            .AddType<GetUserPagedInput>()
            .AddType<CreateUserInput>()
            .AddType<ActivateUserInput>()
            .AddType<InactivateUserInput>()
            .AddType<GenderType>()
            .AddType<UserStatus>();

        return builder;
    }

    private static IRequestExecutorBuilder AddDirectives(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddDirectiveType<FormatCpfDirective>()
            .AddDirectiveType<LowerDirective>()
            .AddDirectiveType<UpperDirective>();

        return builder;
    }
}