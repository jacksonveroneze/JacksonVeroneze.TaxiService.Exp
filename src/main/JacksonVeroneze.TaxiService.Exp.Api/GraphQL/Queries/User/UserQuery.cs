using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Queries.User;

public class UserQuery
{
    public async Task<ICollection<UserResponse>> GetUsersAsync(
        [FromServices] ISender mediator,
        UserStatus? status,
        CancellationToken cancellationToken)
    {
        GetUserPagedQuery query = new()
        {
            Status = status
        };

        NET.Result.Result<GetUserPagedQueryResponse>? result =
            await mediator.Send(query, cancellationToken);

        return result.Value?.Data ?? [];
    }

    public async Task<UserResponse> GetUserByIdAsync(
        [FromServices] ISender mediator,
        Guid? id,
        CancellationToken cancellationToken)
    {
        GetUserByIdQuery query = new(id!.Value);

        NET.Result.Result<GetUserByIdQueryResponse> result =
            await mediator.Send(query, cancellationToken);

        return result.Value?.Data!;
    }
}