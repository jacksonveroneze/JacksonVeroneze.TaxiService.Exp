using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Validators;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Services;

public class PassengerValidator(
    IPassengerRepository repository,
    ILogger<PassengerValidator> logger)
    : IPassengerValidator
{
    public async Task<Result> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        (PassengerEntity? entity, Error? error) =
            await InternalGetAsync(id, cancellationToken);

        return entity is not null
            ? Result.WithSuccess()
            : Result.FromNotFound(error!);
    }

    public async Task<Result<PassengerEntity>> ValidateAndGetAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        (PassengerEntity? entity, Error? error) =
            await InternalGetAsync(id, cancellationToken);

        return entity is not null
            ? Result<PassengerEntity>.WithSuccess(entity)
            : Result<PassengerEntity>.FromNotFound(error!);
    }

    private async Task<(PassengerEntity?, Error?)> InternalGetAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassengerEntity? entity = await repository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return (entity, null);
        }

        Error error = DomainErrors.PassengerError.NotFound;

        logger.LogNotFound(
            nameof(PassengerValidator),
            nameof(ValidateAndGetAsync), id, error);

        return (null, error);
    }
}
