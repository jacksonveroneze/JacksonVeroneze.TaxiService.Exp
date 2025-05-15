using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Services;

public class DriverService(
    ILogger<DriverService> logger,
    IDriverRepository driverRepository)
    : IDriverService
{
    public async Task<Result<bool>> ExistsAndIsActiveAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Result<DriverEntity> entity = await InternalGetByIdAsync(
            id, cancellationToken);

        if (entity.IsFailure)
        {
            return Result<bool>.FromNotFound(entity.FirstError!);
        }

        if (entity.Value!.IsActive)
        {
            return Result<bool>.WithSuccess();
        }

        Error errorNotActivated = DomainErrors.DriverErrors.NotActivated;

        logger.LogGenericValidationError(
            nameof(DriverService),
            nameof(ExistsAndIsActiveAsync), errorNotActivated);

        return Result<bool>.WithError(errorNotActivated);
    }

    public Task<Result<DriverEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return InternalGetByIdAsync(id, cancellationToken);
    }

    public async Task<Result<bool>> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Result<DriverEntity> entity = await InternalGetByIdAsync(
            id, cancellationToken);

        return Result<bool>.WithSuccess(entity.IsSuccess);
    }

    private async Task<Result<DriverEntity>> InternalGetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        DriverEntity? entity = await driverRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            logger.LogGetById(
                ModuleContext.ContextName,
                nameof(DriverService),
                nameof(InternalGetByIdAsync), id);

            return Result<DriverEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.DriverErrors.NotFound;

        logger.LogNotFound(
            nameof(DriverService),
            nameof(GetByIdAsync), id, error);

        return Result<DriverEntity>.FromNotFound(error);
    }
}
