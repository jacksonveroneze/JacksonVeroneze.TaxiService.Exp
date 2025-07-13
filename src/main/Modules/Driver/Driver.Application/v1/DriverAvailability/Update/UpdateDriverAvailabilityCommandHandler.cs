using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Services;
using TaxiService.Modules.Driver.Application.v1.DriverAvailability.Common.Models;
using TaxiService.Modules.Driver.Domain;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Domain.Repositories;
using TaxiService.Modules.Driver.Domain.Services;

namespace TaxiService.Modules.Driver.Application.v1.DriverAvailability.Update;

public sealed class UpdateDriverAvailabilityCommandHandler(
    ILogger<UpdateDriverAvailabilityCommandHandler> logger,
    IDriverService driverService,
    IDriverAvailabilityDomainService driverAvailabilityDomainService,
    IDriverAvailabilityHistoryReadRepository driverAvailabilityHistoryReadRepository,
    IDriverAvailabilityHistoryRepository driverAvailabilityHistoryRepository)
    : IRequestHandler<UpdateDriverAvailabilityCommand, Result>
{
    public async Task<Result> Handle(
        UpdateDriverAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result resultValidateDriver = await driverService
            .ExistsAndIsActiveAsync(request.Id, cancellationToken);

        if (resultValidateDriver.IsFailure)
        {
            return resultValidateDriver;
        }

        Result<DriverAvailabilityHistory> driverAvailability =
            await FactoryEntityAsync(request, cancellationToken);

        if (driverAvailability.IsFailure)
        {
            return driverAvailability;
        }

        await CreateAsync(driverAvailability.Value!, cancellationToken);

        return Result.WithSuccess();
    }


    private async Task<Result<DriverAvailabilityHistory>> FactoryEntityAsync(
        UpdateDriverAvailabilityCommand request, CancellationToken cancellationToken)
    {
        DriverAvailabilityDto? lastAvailability =
            await driverAvailabilityHistoryReadRepository
                .GetLastByDriverIdAsync(request.Id, cancellationToken);

        Result<DriverAvailabilityHistory> entity = driverAvailabilityDomainService
            .TryUpdateAvailabilityStatus(
                request.Id, lastAvailability?.Status, request.Status);

        if (entity.IsFailure)
        {
            logger.LogGenericError(
                ModuleConstants.ContextName,
                nameof(UpdateDriverAvailabilityCommandHandler),
                nameof(FactoryEntityAsync), entity.Errors.Count);
        }

        return entity;
    }

    private async Task CreateAsync(
        DriverAvailabilityHistory entity,
        CancellationToken cancellationToken)
    {
        await driverAvailabilityHistoryRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(
            ModuleConstants.ContextName,
            nameof(UpdateDriverAvailabilityCommandHandler),
            nameof(CreateAsync), entity.Id);
    }
}
