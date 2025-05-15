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

        Result<bool> result = await driverService
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
            return Result<UpdateDriverLocationHandler>
                .FromInvalid(driverLocation.Errors!);
        }

        await CreateAsync(driverLocation.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result<DriverLocationEntity> FactoryEntity(
        UpdateDriverLocationCommand request)
    {
        Result<DriverLocationEntity> entity =
            DriverLocationEntity.Create(
                request.Id, request.Latitude, request.Longitude);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(UpdateDriverLocationHandler),
            nameof(FactoryEntity), entity.Errors!.Count);

        return Result<DriverLocationEntity>
            .FromInvalid(entity.Errors!);
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
