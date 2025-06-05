using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Services;
using TaxiService.Modules.Driver.Domain;
using TaxiService.Modules.Driver.Domain.Core.Errors;
using TaxiService.Modules.Driver.Domain.Repositories;

namespace TaxiService.Modules.Driver.Application.Common.Services;

public class DriverService(
    ILogger<DriverService> logger,
    IDriverRepository driverRepository)
    : IDriverService
{
    public async Task<Result> ExistsAndIsActiveAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        Domain.Aggregates.Driver? entity = await driverRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<Domain.Aggregates.Driver>.WithSuccess(entity);
        }

        if (entity!.IsActive)
        {
            return Result.WithSuccess();
        }

        Error errorNotActivated = DomainErrors.DriverErrors.NotActivated;

        logger.LogGenericValidationError(
            ModuleConstants.ContextName,
            nameof(DriverService),
            nameof(ExistsAndIsActiveAsync), errorNotActivated);

        return Result.FromRuleViolation(errorNotActivated);
    }
}
