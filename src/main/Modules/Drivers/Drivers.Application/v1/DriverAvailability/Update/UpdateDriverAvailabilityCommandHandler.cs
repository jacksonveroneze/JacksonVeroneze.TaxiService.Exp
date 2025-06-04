using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Enums;
using TaxiService.Modules.Drivers.Domain.Repositories;

namespace TaxiService.Modules.Drivers.Application.v1.DriverAvailability.Update;

public sealed class UpdateDriverAvailabilityCommandHandler(
    ILogger<UpdateDriverAvailabilityCommandHandler> logger,
    IDriverService driverService,
    IDriverAvailabilityHistoryReadRepository driverAvailabilityHistoryReadRepository,
    IDriverAvailabilityHistoryRepository driverAvailabilityHistoryRepository)
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

        Result<DriverAvailabilityHistory> driverAvailability =
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

        DriverAvailabilityHistory? lastAvailability =
            await driverAvailabilityHistoryReadRepository
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

    private Result<DriverAvailabilityHistory> FactoryEntity(
        UpdateDriverAvailabilityCommand request)
    {
        Result<DriverAvailabilityHistory> entity =
            DriverAvailabilityHistory.Create(
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
        DriverAvailabilityHistory entity,
        CancellationToken cancellationToken)
    {
        await driverAvailabilityHistoryRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(nameof(UpdateDriverAvailabilityCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}
