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
    public async Task<Result> ExistsAndIsActiveAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        DriverEntity? entity = await driverRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<DriverEntity>.WithSuccess(entity);
        }

        if (entity!.IsActive)
        {
            return Result.WithSuccess();
        }

        Error errorNotActivated = DomainErrors.DriverErrors.NotActivated;

        logger.LogGenericValidationError(
            nameof(DriverService),
            nameof(ExistsAndIsActiveAsync), errorNotActivated);

        return Result.FromRuleViolation(errorNotActivated);
    }
}
