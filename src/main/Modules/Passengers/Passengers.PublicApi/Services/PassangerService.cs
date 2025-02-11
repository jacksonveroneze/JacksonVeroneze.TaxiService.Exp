using JacksonVeroneze.NET.Result;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;

namespace TaxiService.Modules.Passengers.PublicApi.Services;

public class PassangerService(
    ILogger<PassangerService> logger,
    IPassangerReadRepository passangerReadRepository) : IPassangerService
{
    public async Task<Result<bool>> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassangerEntity? entity = await passangerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<bool>.WithSuccess(true);
        }

        Error error = DomainErrors.PassangerError.NotFound;

        logger.LogNotFound(
            nameof(PassangerService),
            nameof(ExistsByIdAsync), id, error);

        return Result<bool>.FromNotFound(error);
    }
}