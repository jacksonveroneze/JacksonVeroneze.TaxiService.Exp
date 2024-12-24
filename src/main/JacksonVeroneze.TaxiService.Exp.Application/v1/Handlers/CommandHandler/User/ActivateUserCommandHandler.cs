using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.System;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.User;

public sealed class ActivateUserCommandHandler(
    ILogger<ActivateUserCommandHandler> logger,
    IUserReadRepository userReadRepository,
    IUserWriteRepository userWriteRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<ActivateUserCommand, Result>
{
    public async Task<Result> Handle(
        ActivateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        (UserEntity? Entity, Error? Error) userResult =
            await GetUserByIdAsync(request.Id, cancellationToken);

        if (userResult.Entity is null)
        {
            return Result<ActivateUserCommandHandler>
                .FromNotFound(userResult.Error!);
        }

        Result resultAction = Activate(userResult.Entity!);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(userResult.Entity!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<(UserEntity?, Error?)> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        UserEntity? entity = await userReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return (entity, null);
        }

        Error error = DomainErrors.UserError.NotFound;

        logger.LogNotFound(
            nameof(ActivateUserCommandHandler),
            nameof(GetUserByIdAsync), id, error);

        return (null, error);
    }

    private Result Activate(UserEntity entity)
    {
        Result result = entity.Activate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(
                nameof(ActivateUserCommandHandler),
                nameof(Activate), entity.Id, result.Error!);
        }

        return result;
    }

    private async Task UpdateAsync(
        UserEntity entity,
        CancellationToken cancellationToken)
    {
        await userWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(ActivateUserCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}