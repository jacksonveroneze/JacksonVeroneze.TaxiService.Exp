using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Validators;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Services;

public class PassengerValidator(
    IPassangerReadRepository readRepository,
    ILogger<PassengerValidator> logger)
    : IPassengerValidator
{
    public async Task<Result> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        (PassangerEntity? entity, Error? error) =
            await InternalGetAsync(id, cancellationToken);

        return entity is not null
            ? Result.WithSuccess()
            : Result.FromNotFound(error!);
    }

    public async Task<Result<PassangerEntity>> ValidateAndGetAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        (PassangerEntity? entity, Error? error) =
            await InternalGetAsync(id, cancellationToken);

        return entity is not null
            ? Result<PassangerEntity>.WithSuccess(entity)
            : Result<PassangerEntity>.FromNotFound(error!);
    }

    private async Task<(PassangerEntity?, Error?)> InternalGetAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassangerEntity? entity = await readRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return (entity, null);
        }

        Error error = DomainErrors.PassangerError.NotFound;

        logger.LogNotFound(
            nameof(PassengerValidator),
            nameof(ValidateAndGetAsync), id, error);

        return (null, error);
    }
}