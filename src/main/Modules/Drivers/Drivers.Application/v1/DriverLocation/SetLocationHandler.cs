using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverLocation;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.DriverLocation;

public sealed class SetLocationHandler(
    ILogger<SetLocationHandler> logger,
    IDriverService driverService,
    IDriverLocationWriteRepository driverLocationWriteRepository)
    : IRequestHandler<SetLocationCommand, Result>
{
    public async Task<Result> Handle(
        SetLocationCommand request,
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
            return Result<SetLocationHandler>
                .FromInvalid(driverLocation.Errors!);
        }

        await CreateAsync(driverLocation.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result<DriverLocationEntity> FactoryEntity(
        SetLocationCommand request)
    {
        Result<DriverLocationEntity> entity =
            DriverLocationEntity.Create(
                request.Id, request.Latitude, request.Longitude);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(SetLocationHandler),
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

        logger.LogCreated(nameof(SetLocationHandler),
            nameof(CreateAsync), entity.Id);
    }
}