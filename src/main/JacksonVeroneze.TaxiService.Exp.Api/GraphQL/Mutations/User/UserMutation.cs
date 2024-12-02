using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Mutations.User;

public class UserMutation
{
    public async Task<UserResponse> AddUserAsync(
        [FromServices] ISender mediator,
        CreateUserCommand input,
        CancellationToken cancellationToken)
    {
        NET.Result.Result<CreateUserCommandResponse> result =
            await mediator.Send(input, cancellationToken);

        return result.Value?.Data!;
    }
}