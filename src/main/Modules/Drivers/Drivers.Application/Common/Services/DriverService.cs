using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Aggregates;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Repositories;

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
        Driver? entity = await driverRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<Driver>.WithSuccess(entity);
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
