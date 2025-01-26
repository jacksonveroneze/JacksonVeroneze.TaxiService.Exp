using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Common;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Inputs;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using MediatR;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Queries.User;

internal static class UserQueryType
{
    private const string PagedName = "getUsersPaged";
    private const string ByIdName = "getUserById";

    public static IObjectTypeDescriptor AddUserQueries(
        this IObjectTypeDescriptor descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor
            .Field(PagedName)
            .Argument(Constants.INPUT, input => input.Type<GetUserPagedInput>())
            .Type<NonNullType<ListType<NonNullType<Types.User.User>>>>()
            .Resolve(async context =>
            {
                ISender mediator = context.RequestServices
                    .GetService<ISender>()!;

                GetUserPagedQuery query = context
                    .ArgumentValue<GetUserPagedQuery>(Constants.INPUT);

                NET.Result.Result<GetUserPagedQueryResponse>? result =
                    await mediator.Send(query);

                return result.Value?.Data;
            });

        descriptor
            .Field(ByIdName)
            .Argument(Constants.INPUT, input => input.Type<GetUserByIdInput>())
            .Type<NonNullType<NonNullType<Types.User.User>>>()
            .Resolve(async context =>
            {
                ISender mediator = context.RequestServices
                    .GetService<ISender>()!;

                GetUserByIdQuery query = context
                    .ArgumentValue<GetUserByIdQuery>(Constants.INPUT);

                NET.Result.Result<GetUserByIdQueryResponse>? result =
                    await mediator.Send(query);

                return result.Value?.Data;
            });

        return descriptor;
    }
}