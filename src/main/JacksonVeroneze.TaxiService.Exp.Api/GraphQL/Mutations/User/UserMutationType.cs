using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Common;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using MediatR;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Mutations.User;

internal static class UserMutationType
{
    private const string createName = "createUser";
    private const string activateName = "activateUser";

    public static IObjectTypeDescriptor AddUserMutations(
        this IObjectTypeDescriptor descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor
            .Field(createName)
            .Argument(Constants.INPUT, arg => arg.Type<NonNullType<CreateUserInputType>>())
            .Type<NonNullType<UserResponseType>>()
            .Resolve(async context =>
            {
                ISender mediator = context.RequestServices
                    .GetService<ISender>()!;

                CreateUserCommand command = context
                    .ArgumentValue<CreateUserCommand>(Constants.INPUT);

                NET.Result.Result<CreateUserCommandResponse> result =
                    await mediator.Send(command, context.RequestAborted);

                return result.Match(
                    () => result.Value!.Data,
                    () => null, context);
            });

        descriptor
            .Field(activateName)
            .Argument(Constants.INPUT, arg => arg.Type<NonNullType<ActivateUserInputType>>())
            .Type<NonNullType<BooleanType>>()
            .Resolve(async context =>
            {
                ISender mediator = context.RequestServices
                    .GetService<ISender>()!;

                ActivateUserCommand command = context
                    .ArgumentValue<ActivateUserCommand>(Constants.INPUT);

                NET.Result.Result result = await mediator
                    .Send(command, context.RequestAborted);

                return result.Match(
                    () => result.IsSuccess,
                    () => result.IsSuccess, context);
            });

        return descriptor;
    }
}