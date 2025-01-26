using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Common;
using JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Types.User.Inputs;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using MediatR;

namespace JacksonVeroneze.TaxiService.Exp.Api.GraphQL.Mutations.User;

internal static class UserMutationType
{
    private const string createName = "createUser";
    private const string activateName = "activateUser";
    private const string inactivateName = "inactivateUser";

    public static IObjectTypeDescriptor AddUserMutations(
        this IObjectTypeDescriptor descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        descriptor
            .Field(createName)
            .Argument(Constants.INPUT, arg => arg.Type<NonNullType<CreateUserInput>>())
            .Type<NonNullType<Types.User.User>>()
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
            .Argument(Constants.INPUT, arg => arg.Type<NonNullType<ActivateUserInput>>())
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

        descriptor
            .Field(inactivateName)
            .Argument(Constants.INPUT, arg => arg.Type<NonNullType<InactivateUserInput>>())
            .Type<NonNullType<BooleanType>>()
            .Resolve(async context =>
            {
                ISender mediator = context.RequestServices
                    .GetService<ISender>()!;

                InactivateUserCommand command = context
                    .ArgumentValue<InactivateUserCommand>(Constants.INPUT);

                NET.Result.Result result = await mediator
                    .Send(command, context.RequestAborted);

                return result.Match(
                    () => result.IsSuccess,
                    () => result.IsSuccess, context);
            });

        return descriptor;
    }
}