using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverLocation;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.DriverLocation.Update;

public sealed class UpdateDriverLocationHandler(
    ILogger<UpdateDriverLocationHandler> logger,
    IDriverService driverService,
    IDriverLocationWriteRepository driverLocationWriteRepository)
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

        Result<DriverLocationEntity> driverLocation =
            FactoryEntity(request);

        if (driverLocation.IsFailure)
        {
            return driverLocation;
        }

        await CreateAsync(driverLocation.Value!, 
            cancellationToken);

        return Result.WithSuccess();
    }

    private Result<DriverLocationEntity> FactoryEntity(
        UpdateDriverLocationCommand request)
    {
        Result<DriverLocationEntity> resultEntity =
            DriverLocationEntity.Create(request.Id,
                request.Latitude, request.Longitude);

        if (!resultEntity.IsSuccess)
        {
            logger.LogGenericError(
                nameof(UpdateDriverLocationHandler),
                nameof(FactoryEntity), resultEntity.Errors.Count);
        }

        return resultEntity;
    }

    private async Task CreateAsync(
        DriverLocationEntity entity,
        CancellationToken cancellationToken)
    {
        await driverLocationWriteRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(nameof(UpdateDriverLocationHandler),
            nameof(CreateAsync), entity.Id);
    }
}
