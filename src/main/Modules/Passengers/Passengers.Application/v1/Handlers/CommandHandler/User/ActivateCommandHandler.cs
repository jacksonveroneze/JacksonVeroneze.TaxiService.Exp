using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Passengers.Application.v1.Commands.User;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.User;

public sealed class ActivateCommandHandler(
    ILogger<ActivateCommandHandler> logger,
    IUserReadRepository userReadRepository,
    IUserWriteRepository userWriteRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<ActivateCommand, Result>
{
    public async Task<Result> Handle(
        ActivateCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<UserEntity> resultEntity = await GetUserByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        Result resultAction = Activate(resultEntity.Value!);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result<UserEntity>> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        UserEntity? entity = await userReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<UserEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.UserError.NotFound;

        logger.LogNotFound(
            nameof(InactivateCommandHandler),
            nameof(GetUserByIdAsync), id, error);

        return Result<UserEntity>.FromNotFound(error);
    }

    private Result Activate(UserEntity entity)
    {
        Result result = entity.Activate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(
                nameof(ActivateCommandHandler),
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
            nameof(ActivateCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}