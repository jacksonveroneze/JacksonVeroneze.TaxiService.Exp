using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using MediatR;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Queries.User;

public class UserQueryType : ObjectType
{
    protected override void Configure(
        IObjectTypeDescriptor descriptor)
    {
        base.Configure(descriptor);

        descriptor.Name(OperationTypeNames.Query);

        descriptor
            .Field("paged")
            .Argument("input", input => input.Type<GetUserPagedQueryInputType>())
            .Type<NonNullType<ListType<NonNullType<UserResponseType>>>>()
            .Resolve(async context =>
            {
                IMediator mediator = context.RequestServices
                    .GetService<IMediator>()!;

                GetUserPagedQuery query = context
                    .ArgumentValue<GetUserPagedQuery>("input");

                NET.Result.Result<GetUserPagedQueryResponse>? result =
                    await mediator.Send(query);

                return result.Value?.Data;
            });

        descriptor
            .Field("byId")
            .Argument("input", input => input.Type<GetUserByIdQueryInputType>())
            .Type<NonNullType<UserResponseType>>()
            .Resolve(async context =>
            {
                IMediator mediator = context.RequestServices
                    .GetService<IMediator>()!;

                GetUserByIdQuery query = context
                    .ArgumentValue<GetUserByIdQuery>("input");

                NET.Result.Result<GetUserByIdQueryResponse>? result =
                    await mediator.Send(query);

                return result.Value?.Data;
            });
    }
}