using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverLocation;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.DriverLocation.Update;

public sealed class UpdateDriverLocationCommandHandler(
    ILogger<UpdateDriverLocationCommandHandler> logger,
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
                nameof(UpdateDriverLocationCommandHandler),
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

        logger.LogCreated(nameof(UpdateDriverLocationCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}
