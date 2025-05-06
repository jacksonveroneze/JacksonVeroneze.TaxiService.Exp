using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverLocation;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.DriverLocation.Update;

public sealed class UpdateHandler(
    ILogger<UpdateHandler> logger,
    IDriverService driverService,
    IDriverLocationWriteRepository driverLocationWriteRepository)
    : IRequestHandler<UpdateCommand, Result>
{
    public async Task<Result> Handle(
        UpdateCommand request,
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
            return Result<UpdateHandler>
                .FromInvalid(driverLocation.Errors!);
        }

        await CreateAsync(driverLocation.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result<DriverLocationEntity> FactoryEntity(
        UpdateCommand request)
    {
        Result<DriverLocationEntity> entity =
            DriverLocationEntity.Create(
                request.Id, request.Latitude, request.Longitude);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(UpdateHandler),
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<DriverLocationEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task CreateAsync(
        DriverLocationEntity entity,
        CancellationToken cancellationToken)
    {
        await driverLocationWriteRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(nameof(UpdateHandler),
            nameof(CreateAsync), entity.Id);
    }
}
