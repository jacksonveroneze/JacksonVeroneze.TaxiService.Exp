using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Services;

public class DriverService(
    ILogger<InactivateCommandHandler> logger,
    IDriverRepository driverRepository)
    : IDriverService
{
    public async Task<Result<bool>> ExistsAndIsActiveAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        DriverEntity? entity = await InternalGetByIdAsync(
            id, cancellationToken);

        if (entity is null)
        {
            Error error = DomainErrors.DriverErrors.NotFound;

            return Result<bool>.FromNotFound(error);
        }

        if (entity.IsActive)
        {
            return Result<bool>.WithSuccess();
        }

        Error errorNotActivated = DomainErrors.DriverErrors.NotActivated;

        logger.LogGenericValidationError(
            nameof(DriverService),
            nameof(ExistsAndIsActiveAsync), errorNotActivated);

        return Result<bool>.WithError(errorNotActivated);
    }

    public async Task<Result<DriverEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        DriverEntity? entity = await InternalGetByIdAsync(
            id, cancellationToken);

        if (entity is not null)
        {
            return Result<DriverEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.DriverErrors.NotFound;

        return Result<DriverEntity>.FromNotFound(error);
    }

    public async Task<Result<bool>> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        DriverEntity? entity = await InternalGetByIdAsync(
            id, cancellationToken);

        return Result<bool>.WithSuccess(entity is not null);
    }

    private async Task<DriverEntity?> InternalGetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        DriverEntity? entity = await driverRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            logger.LogGetById(
                nameof(DriverService),
                nameof(InternalGetByIdAsync), id);
            
            return entity;
        }

        Error error = DomainErrors.DriverErrors.NotFound;

        logger.LogNotFound(
            nameof(ActivateCommandHandler),
            nameof(GetByIdAsync), id, error);

        return null;
    }
}