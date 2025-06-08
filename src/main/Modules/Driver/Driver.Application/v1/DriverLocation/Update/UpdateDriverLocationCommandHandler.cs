using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Services;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Filters;
using TaxiService.Modules.Driver.Domain;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Domain.Repositories;

namespace TaxiService.Modules.Driver.Application.v1.DriverLocation.Update;

public sealed class UpdateDriverLocationCommandHandler(
    ILogger<UpdateDriverLocationCommandHandler> logger,
    IDriverService driverService,
    IDriverLocationHistoryRepository driverLocationHistoryRepository)
    : IRequestHandler<UpdateDriverLocationCommand, Result>
{
    public async Task<Result> Handle(
        UpdateDriverLocationCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result result = await driverService
            .ExistsAndIsActiveAsync(request.Id,
                cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        Result<DriverLocationHistory> driverLocation =
            FactoryEntity(request);

        if (driverLocation.IsFailure)
        {
            return driverLocation;
        }

        await CreateAsync(driverLocation.Value!,
            cancellationToken);

        return Result.WithSuccess();
    }

    private Result<DriverLocationHistory> FactoryEntity(
        UpdateDriverLocationCommand request)
    {
        Result<DriverLocationHistory> resultEntity =
            DriverLocationHistory.Create(request.Id,
                request.Latitude, request.Longitude);

        if (!resultEntity.IsSuccess)
        {
            logger.LogGenericError(
                ModuleConstants.ContextName,
                nameof(UpdateDriverLocationCommandHandler),
                nameof(FactoryEntity), resultEntity.Errors.Count);
        }

        return resultEntity;
    }

    private async Task CreateAsync(
        DriverLocationHistory entity,
        CancellationToken cancellationToken)
    {
        await driverLocationHistoryRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(
            ModuleConstants.ContextName,
            nameof(UpdateDriverLocationCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}
