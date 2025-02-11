using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Passengers.Application.v1.Commands.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Passanger;

public sealed class ActivateCommandHandler(
    ILogger<ActivateCommandHandler> logger,
    IPassangerReadRepository passangerReadRepository,
    IPassangerWriteRepository passangerWriteRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<ActivateCommand, Result>
{
    public async Task<Result> Handle(
        ActivateCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassangerEntity> resultEntity = await GetByIdAsync(
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

    private async Task<Result<PassangerEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassangerEntity? entity = await passangerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<PassangerEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.PassangerError.NotFound;

        logger.LogNotFound(
            nameof(InactivateCommandHandler),
            nameof(GetByIdAsync), id, error);

        return Result<PassangerEntity>.FromNotFound(error);
    }

    private Result Activate(PassangerEntity entity)
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
        PassangerEntity entity,
        CancellationToken cancellationToken)
    {
        await passangerWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(ActivateCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}