using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.v1.Commands.Vehicle;
using TaxiService.Modules.Drivers.Application.v1.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Vehicle;
using TaxiService.Modules.Drivers.Application.v1.Models.Vehicle;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.Vehicle;

public sealed class CreateCommandHandler(
    ILogger<CreateCommandHandler> logger,
    IDataMapper mapper,
    IDriverReadRepository driverReadRepository,
    IVehicleReadRepository vehicleReadRepository,
    IVehicleWriteRepository vehicleWriteRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCommand, Result<CreateCommandResponse>>
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public async Task<Result<CreateCommandResponse>> Handle(
        CreateCommand request,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        ArgumentNullException.ThrowIfNull(request);

        Result validationResult = await ValidateRequestAsync(
            request, cancellationToken);

        if (validationResult.IsFailure)
        {
            return Result<CreateCommandResponse>
                .WithError(validationResult.Errors!);
        }

        Result<VehicleEntity> vehicleEntity = FactoryEntity(request);

        if (vehicleEntity.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(vehicleEntity.Errors!);
        }

        await CreateAsync(vehicleEntity.Value!,
            cancellationToken);

        CreateCommandResponse response =
            mapper.Map<CreateCommandResponse>(vehicleEntity.Value);

        return Result<CreateCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ValidateRequestAsync(
        CreateCommand request,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result plateResult = await ValidateVehiclePlateAsync(
            request.Plate!, cancellationToken);

        Result driverResult = await ValidateDriverVehicleAsync(
            request.DriverId, cancellationToken);

        Result driverVehicleResult = await ValidateDriverAsync(
            request.DriverId, cancellationToken);

        return Result.FailuresOrSuccess(
            plateResult,
            driverResult,
            driverVehicleResult);
    }

    private async Task<Result> ValidateVehiclePlateAsync(
        string email,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        bool exists = await vehicleReadRepository
            .ExistsByPlateAsync(email, cancellationToken);

        if (!exists)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.VehicleErrors.DuplicatePlate;

        logger.LogAlreadyExists(nameof(CreateCommandHandler),
            nameof(ValidateVehiclePlateAsync), email, error);

        return Result.FromInvalid(error);
    }

    private async Task<Result> ValidateDriverVehicleAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        bool exists = await vehicleReadRepository
            .ExistVehicleByDriverAsync(driverId, cancellationToken);

        if (!exists)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.VehicleErrors.DuplicateDriverId;

        logger.LogAlreadyExists(nameof(CreateCommandHandler),
            nameof(ValidateVehiclePlateAsync), driverId, error);

        return Result.FromInvalid(error);
    }

    private async Task<Result> ValidateDriverAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        DriverEntity? driverEntity = await driverReadRepository
            .GetByIdAsync(driverId, cancellationToken);

        if (driverEntity is null)
        {
            Error errorNotFound = DomainErrors.DriverErrors.NotFound;

            logger.LogNotFound(nameof(CreateCommandHandler),
                nameof(ValidateVehiclePlateAsync), driverId, errorNotFound);

            return Result.FromInvalid(errorNotFound);
        }

        if (driverEntity.Status is DriverStatus.Active)
        {
            return Result.WithSuccess();
        }

        Error errorInvalidState = DomainErrors.DriverErrors.NotActivated;

        logger.LogGenericValidationError(nameof(CreateCommandHandler),
            nameof(ValidateVehiclePlateAsync), errorInvalidState);

        return Result.FromInvalid(errorInvalidState);
    }

    private Result<VehicleEntity> FactoryEntity(
        CreateCommand request)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<VehicleEntity> entity = VehicleEntity.Create(
            request.DriverId, request.Model,
            request.Brand!, request.Plate!,
            request.Color, request.Year);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateCommandHandler),
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<VehicleEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task CreateAsync(
        VehicleEntity vehicleEntity,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        await vehicleWriteRepository.CreateAsync(
            vehicleEntity, cancellationToken);

        await unitOfWork.CommitAsync(
            cancellationToken);

        logger.LogCreated(nameof(CreateCommandHandler),
            nameof(CreateAsync), vehicleEntity.Id);
    }
}