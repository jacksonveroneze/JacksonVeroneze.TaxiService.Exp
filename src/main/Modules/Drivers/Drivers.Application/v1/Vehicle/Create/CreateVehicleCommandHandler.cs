using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.Common.Interfaces;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Aggregates;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Repositories;

using IVehicleReadRepository = TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.IVehicleReadRepository;

namespace TaxiService.Modules.Drivers.Application.v1.Vehicle.Create;

public sealed class CreateVehicleCommandHandler(
    ILogger<CreateVehicleCommandHandler> logger,
    IDataMapper mapper,
    IDriverService driverService,
    IVehicleReadRepository vehicleReadRepository,
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateVehicleCommand, Result<CreateVehicleCommandResponse>>
{
    public async Task<Result<CreateVehicleCommandResponse>> Handle(
        CreateVehicleCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result validationResult = await ValidateBussinesRulesAsync(
            request, cancellationToken);

        if (validationResult.IsFailure)
        {
            return Result<CreateVehicleCommandResponse>
                .FromInvalid(validationResult.Errors);
        }

        Result<Domain.Aggregates.Vehicle> vehicleEntity = FactoryEntity(request);

        if (vehicleEntity.IsFailure)
        {
            return Result<CreateVehicleCommandResponse>
                .FromInvalid(vehicleEntity.Errors);
        }

        await CreateAsync(vehicleEntity.Value!,
            cancellationToken);

        CreateVehicleCommandResponse response =
            mapper.Map<CreateVehicleCommandResponse>(vehicleEntity.Value);

        return Result<CreateVehicleCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ValidateBussinesRulesAsync(
        CreateVehicleCommand request,
        CancellationToken cancellationToken)
    {
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
        string plate,
        CancellationToken cancellationToken)
    {
        bool exists = await vehicleReadRepository
            .ExistsByPlateAsync(plate, cancellationToken);

        if (!exists)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.VehicleErrors.DuplicatePlate;

        logger.LogAlreadyExists(nameof(CreateVehicleCommandHandler),
            nameof(ValidateVehiclePlateAsync), plate, error);

        return Result.FromInvalid(error);
    }

    private async Task<Result> ValidateDriverVehicleAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        bool exists = await vehicleReadRepository
            .ExistVehicleByDriverAsync(driverId, cancellationToken);

        if (!exists)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.VehicleErrors.DuplicateDriverId;

        logger.LogAlreadyExists(nameof(CreateVehicleCommandHandler),
            nameof(ValidateVehiclePlateAsync), driverId, error);

        return Result.FromInvalid(error);
    }

    private async Task<Result> ValidateDriverAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        Result result = await driverService
            .ExistsAndIsActiveAsync(driverId, cancellationToken);

        return result;
    }

    private Result<Domain.Aggregates.Vehicle> FactoryEntity(
        CreateVehicleCommand request)
    {
        Result<Domain.Aggregates.Vehicle> entity = Domain.Aggregates.Vehicle.Create(
            request.DriverId, request.Model,
            request.Brand!, request.Plate!,
            request.Color, request.Year);

        if (entity.IsFailure)
        {
            logger.LogGenericError(
                nameof(CreateVehicleCommandHandler),
                nameof(FactoryEntity), entity.Errors.Count);
        }

        return entity;
    }

    private async Task CreateAsync(
        Domain.Aggregates.Vehicle vehicle,
        CancellationToken cancellationToken)
    {
        await vehicleRepository.CreateAsync(
            vehicle, cancellationToken);

        await unitOfWork.CommitAsync(
            cancellationToken);

        logger.LogCreated(nameof(CreateVehicleCommandHandler),
            nameof(CreateAsync), vehicle.Id);
    }
}
