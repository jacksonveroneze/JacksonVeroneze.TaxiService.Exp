using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.DriverLocation;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.DriverLocation;

public sealed class UpdateLocationHandler(
    ILogger<UpdateLocationHandler> logger,
    IDriverService driverService,
    IDriverLocationWriteRepository driverLocationWriteRepository)
    : IRequestHandler<UpdateLocationCommand, Result>
{
    public async Task<Result> Handle(
        UpdateLocationCommand request,
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

        var resultValidation = ValidateDriverStatus(resultEntity.Value!);

        if (resultValidation.IsFailure)
        {
            return resultValidation;
        }

        Result<DriverLocationEntity> driverLocation = 
            FactoryEntity(request);

        if (driverLocation.IsFailure)
        {
            return Result<UpdateLocationHandler>
                .FromInvalid(driverLocation.Errors!);
        }

        await CreateAsync(driverLocation.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result ValidateDriverStatus(
        DriverEntity entity)
    {
        if (entity.IsActive)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.DriverErrors.NotActivated;

        logger.LogGenericValidationError(
            nameof(UpdateLocationHandler),
            nameof(ValidateDriverStatus), error);

        return Result<DriverEntity>.WithError(error);
    }

    private Result<DriverLocationEntity> FactoryEntity(
        UpdateLocationCommand request)
    {
        Result<DriverLocationEntity> entity =
            DriverLocationEntity.Create(
                request.Id, request.Latitude, request.Longitude);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(UpdateLocationHandler),
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

        logger.LogCreated(nameof(UpdateLocationHandler),
            nameof(CreateAsync), entity.Id);
    }
}