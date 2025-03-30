using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.DriverAvailability;

public sealed class SetAvailabilityHandler(
    ILogger<SetAvailabilityHandler> logger,
    IDriverService driverService,
    IDriverAvailabilityReadRepository driverAvailabilityReadRepository,
    IDriverAvailabilityWriteRepository driverAvailabilityWriteRepository)
    : IRequestHandler<SetAvailabilityCommand, Result>
{
    public async Task<Result> Handle(
        SetAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result validationResult = await ValidateBussinesRulesAsync(
            request, cancellationToken);

        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        Result<DriverAvailabilityEntity> driverAvailability =
            FactoryEntity(request);

        if (driverAvailability.IsFailure)
        {
            return Result<SetAvailabilityHandler>
                .FromInvalid(driverAvailability.Errors!);
        }

        await CreateAsync(driverAvailability.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result> ValidateBussinesRulesAsync(
        SetAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        Result<bool> resultValidateDriver = await driverService
            .ExistsAndIsActiveAsync(request.Id,
                cancellationToken);

        if (resultValidateDriver.IsFailure)
        {
            return resultValidateDriver;
        }

        DriverAvailabilityEntity? lastAvailability =
            await driverAvailabilityReadRepository
                .GetLastByDriverIdAsync(request.Id, cancellationToken);

        if (lastAvailability is null)
        {
            if (request.Status is DriverAvailabilityStatus.Online)
            {
                return Result<DriverAvailabilityEntity>
                    .WithSuccess();
            }

            Error error = DomainErrors.DriverAvailabilityError
                .InvalidStatus;

            return Result<DriverAvailabilityEntity>
                .FromInvalid(error);
        }

        var result = request.Status switch
        {
            DriverAvailabilityStatus.Online => lastAvailability.SetOnline(),
            DriverAvailabilityStatus.Offline => lastAvailability.SetOffline(),
            DriverAvailabilityStatus.InRide => lastAvailability.SetInRide(),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (result.IsFailure)
        {
            return Result<DriverAvailabilityEntity>
                .FromInvalid(result.Error!);
        }

        return Result<DriverAvailabilityEntity>
            .WithSuccess();
    }

    private Result<DriverAvailabilityEntity> FactoryEntity(
        SetAvailabilityCommand request)
    {
        Result<DriverAvailabilityEntity> entity =
            DriverAvailabilityEntity.Create(
                request.Id, request.Status);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(SetAvailabilityHandler),
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<DriverAvailabilityEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task CreateAsync(
        DriverAvailabilityEntity entity,
        CancellationToken cancellationToken)
    {
        await driverAvailabilityWriteRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(nameof(SetAvailabilityHandler),
            nameof(CreateAsync), entity.Id);
    }
}