using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Activate;

public sealed class ActivateDriverCommandHandler(
    ILogger<ActivateDriverCommandHandler> logger,
    IDriverService driverService,
    IDriverRepository driverRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<ActivateDriverCommand, Result>
{
    public async Task<Result> Handle(
        ActivateDriverCommand request,
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

        return Result.WithSuccess();
    }

    private Result Activate(DriverEntity entity)
    {
        Result result = entity.Activate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(ActivateDriverCommandHandler),
                nameof(Activate), entity.Id, result.FirstError!);
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
            nameof(ActivateDriverCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}
