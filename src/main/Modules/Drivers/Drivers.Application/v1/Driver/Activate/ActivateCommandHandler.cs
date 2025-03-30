using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Activate;

public sealed class ActivateCommandHandler(
    ILogger<ActivateCommandHandler> logger,
    IDriverService driverService,
    IDriverRepository driverRepository,
    IDateTimeProvider dateTime,
    IPublisher publisher)
    : IRequestHandler<ActivateCommand, Result>
{
    public async Task<Result> Handle(
        ActivateCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<DriverEntity> resultEntity =
            await driverService.GetByIdAsync(
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

        var enumerable = resultEntity.Value?.Events?
            .Select(evt => publisher.Publish(evt, cancellationToken))
            .ToArray();

        if (enumerable != null && enumerable.Any())
            await Task.WhenAll(enumerable);

        return Result.WithSuccess();
    }

    private Result Activate(DriverEntity entity)
    {
        Result result = entity.Activate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(ActivateCommandHandler),
                nameof(Activate), entity.Id, result.Error!);
        }

        return result;
    }

    private async Task UpdateAsync(
        DriverEntity entity,
        CancellationToken cancellationToken)
    {
        await driverRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(ActivateCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}