using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Reactivate;

public sealed class ReactivateDriverCommandHandler(
    ILogger<ReactivateDriverCommandHandler> logger,
    IDriverService driverService,
    IDriverRepository driverRepository)
    : IRequestHandler<ReactivateDriverCommand, Result>
{
    public async Task<Result> Handle(
        ReactivateDriverCommand request,
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

        Result resultAction = Reactivate(resultEntity.Value!);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result Reactivate(DriverEntity entity)
    {
        Result result = entity.Reactivate();

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(ReactivateDriverCommandHandler),
                nameof(Reactivate), entity.Id, result.Error!);
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
            nameof(ReactivateDriverCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}
