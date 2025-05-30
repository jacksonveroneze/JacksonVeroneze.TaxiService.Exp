using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.DriverAvailability.Update;

public sealed class UpdateDriverAvailabilityCommandHandler(
    ILogger<UpdateDriverAvailabilityCommandHandler> logger,
    IDriverService driverService,
    IDriverAvailabilityReadRepository driverAvailabilityReadRepository,
    IDriverAvailabilityWriteRepository driverAvailabilityWriteRepository)
    : IRequestHandler<UpdateDriverAvailabilityCommand, Result>
{
    public async Task<Result> Handle(
        UpdateDriverAvailabilityCommand request,
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
            return driverAvailability;
        }

        await CreateAsync(driverAvailability.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result> ValidateBussinesRulesAsync(
        UpdateDriverAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        Result resultValidateDriver = await driverService
            .ExistsAndIsActiveAsync(request.Id, cancellationToken);

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
                return Result.WithSuccess();
            }

            return Result.FromRuleViolation(
                DomainErrors.DriverAvailabilityError.InvalidStatus);
        }

        Result result = request.Status switch
        {
            DriverAvailabilityStatus.Online => lastAvailability.SetOnline(),
            DriverAvailabilityStatus.Offline => lastAvailability.SetOffline(),
            DriverAvailabilityStatus.InRide => lastAvailability.SetInRide(),
            _ => throw new ArgumentOutOfRangeException(
                nameof(request), request.Status, "Status inválido."),
        };

        return result;
    }

    private Result<DriverAvailabilityEntity> FactoryEntity(
        UpdateDriverAvailabilityCommand request)
    {
        Result<DriverAvailabilityEntity> entity =
            DriverAvailabilityEntity.Create(
                request.Id, request.Status);

        if (entity.IsFailure)
        {
            logger.LogGenericError(
                nameof(UpdateDriverAvailabilityCommandHandler),
                nameof(FactoryEntity), entity.Errors.Count);
        }

        return entity;
    }

    private async Task CreateAsync(
        DriverAvailabilityEntity entity,
        CancellationToken cancellationToken)
    {
        await driverAvailabilityWriteRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(nameof(UpdateDriverAvailabilityCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}
