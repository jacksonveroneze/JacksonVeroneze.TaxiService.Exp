using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.v1.Commands.Vehicle;
using TaxiService.Modules.Drivers.Application.v1.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Vehicle;
using TaxiService.Modules.Drivers.Application.v1.Models.Vehicle;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.Vehicle;

public sealed class CreateCommandHandler(
    ILogger<CreateCommandHandler> logger,
    IDataMapper mapper,
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

        Result resultVerifyEmail = await ExistVehicleAsync(
            request.Plate!, cancellationToken);

        if (resultVerifyEmail.IsFailure)
        {
            return Result<CreateCommandResponse>
                .WithError(resultVerifyEmail.Error!);
        }

        Result<VehicleEntity> userEntity = FactoryEntity(request);

        if (userEntity.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(userEntity.Errors!);
        }

        await CreateAsync(userEntity.Value!,
            cancellationToken);

        CreateCommandResponse response =
            mapper.Map<CreateCommandResponse>(userEntity.Value);

        return Result<CreateCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ExistVehicleAsync(
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
            nameof(ExistVehicleAsync), email, error);

        return Result.FromInvalid(error);
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

        try
        {
            await vehicleWriteRepository.CreateAsync(
                vehicleEntity, cancellationToken);

            await unitOfWork.CommitAsync(
                cancellationToken);

            logger.LogCreated(nameof(CreateCommandHandler),
                nameof(CreateAsync), vehicleEntity!.Id);
        }
        catch (Exception ex)
        {
            logger.LogGenericError(
                nameof(CreateCommandHandler),
                nameof(CreateAsync), ex.Message);

            throw;
        }
    }
}