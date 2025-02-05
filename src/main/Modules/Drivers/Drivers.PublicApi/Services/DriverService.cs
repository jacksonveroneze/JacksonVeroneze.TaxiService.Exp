using JacksonVeroneze.NET.Result;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.PublicApi.Interfaces;

namespace TaxiService.Modules.Drivers.PublicApi.Services;

public class DriverService(
    ILogger<DriverService> logger,
    IDriverReadRepository driverReadRepository) : IDriverService
{
    public async Task<Result<bool>> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        DriverEntity? entity = await driverReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<bool>.WithSuccess(true);
        }

        Error error = DomainErrors.DriverError.NotFound;

        logger.LogNotFound(
            nameof(DriverService),
            nameof(ExistsByIdAsync), id, error);

        return Result<bool>.FromNotFound(error);
    }
}