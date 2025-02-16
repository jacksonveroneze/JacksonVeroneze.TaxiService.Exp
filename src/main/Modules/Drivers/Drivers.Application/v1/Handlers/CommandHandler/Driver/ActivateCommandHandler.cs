using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Drivers.Application.v1.Commands.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.Driver;

public sealed class ActivateCommandHandler(
    ILogger<ActivateCommandHandler> logger,
    IDriverReadRepository driverReadRepository,
    IDriverWriteRepository driverWriteRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<ActivateCommand, Result>
{
    public async Task<Result> Handle(
        ActivateCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<DriverEntity> resultEntity = await GetDriverByIdAsync(
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

    private async Task<Result<DriverEntity>> GetDriverByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        DriverEntity? entity = await driverReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<DriverEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.DriverErrors.NotFound;

        logger.LogNotFound(
            nameof(ActivateCommandHandler),
            nameof(GetDriverByIdAsync), id, error);

        return Result<DriverEntity>.FromNotFound(error);
    }

    private Result Activate(DriverEntity entity)
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
        DriverEntity entity,
        CancellationToken cancellationToken)
    {
        await driverWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(ActivateCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}