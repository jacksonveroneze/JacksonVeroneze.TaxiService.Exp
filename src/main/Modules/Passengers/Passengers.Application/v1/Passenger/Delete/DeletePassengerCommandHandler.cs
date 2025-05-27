using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Delete;

public sealed class DeletePassengerCommandHandler(
    ILogger<DeletePassengerCommandHandler> logger,
    IPassengerRepository passengerRepository)
    : IRequestHandler<DeletePassengerCommand, Result>
{
    public async Task<Result> Handle(
        DeletePassengerCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassengerEntity> resultEntity = await GetByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        await DeleteAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result<PassengerEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassengerEntity? entity = await passengerRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<PassengerEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.PassengerError.NotFound;

        logger.LogNotFound(
            nameof(DeletePassengerCommandHandler),
            nameof(GetByIdAsync), id, error);

        return Result<PassengerEntity>.FromNotFound(error);
    }

    private async Task DeleteAsync(
        PassengerEntity passenger,
        CancellationToken cancellationToken)
    {
        await passengerRepository.DeleteAsync(
            passenger, cancellationToken);

        logger.LogDeleted(nameof(DeletePassengerCommandHandler),
            nameof(Handle), passenger.Id);
    }
}
