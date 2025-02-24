using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.DriverAvailability;

public sealed class UpdateAvailabilityHandler(
    ILogger<UpdateAvailabilityHandler> logger,
    IDriverService driverService,
    IDriverAvailabilityReadRepository driverAvailabilityReadRepository,
    IDriverAvailabilityWriteRepository driverAvailabilityWriteRepository)
    : IRequestHandler<UpdateAvailabilityCommand, Result>
{
    public async Task<Result> Handle(
        UpdateAvailabilityCommand request,
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

        DriverAvailabilityEntity? availability =
            await driverAvailabilityReadRepository
                .GetByDriverIdAsync(request.Id, cancellationToken);

        Task<Result> resultTask = availability is null
            ? CreateAvailabilityAsync(request, cancellationToken)
            : UpdateAvailability(availability, request, cancellationToken);

        Result result = await resultTask;

        logger.LogProcessed(
            nameof(UpdateAvailabilityHandler),
            nameof(CreateAvailabilityAsync), request.Id);

        return result;
    }

    private async Task<Result> CreateAvailabilityAsync(
        UpdateAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        var entity = DriverAvailabilityEntity.Create(
            request.Id, request.Status,
            request.Latitude, request.Longitude);

        if (entity.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(UpdateAvailabilityHandler),
                nameof(UpdateAvailability), entity.Error!);

            return Result.WithError(entity.Error!);
        }

        await driverAvailabilityWriteRepository.UpdateAsync(
            entity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result> UpdateAvailability(
        DriverAvailabilityEntity entity,
        UpdateAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        var result = request.Status switch
        {
            DriverAvailabilityStatus.Online =>
                entity.SetOnline(request.Latitude, request.Longitude),
            DriverAvailabilityStatus.Offline =>
                entity.SetOffline(),
            DriverAvailabilityStatus.InRide =>
                entity.SetInRide(),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(UpdateAvailabilityHandler),
                nameof(UpdateAvailability), result.Error!);

            return Result.WithError(result.Error!);
        }

        await driverAvailabilityWriteRepository.UpdateAsync(
            entity, cancellationToken);


        return Result.WithSuccess();
    }
}