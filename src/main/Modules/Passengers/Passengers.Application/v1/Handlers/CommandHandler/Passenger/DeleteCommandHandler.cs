using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Passenger;

public sealed class DeleteCommandHandler(
    ILogger<DeleteCommandHandler> logger,
    IPassengerReadRepository passengerReadRepository,
    IPassengerWriteRepository passengerWriteRepository)
    : IRequestHandler<DeleteCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCommand request,
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
        PassengerEntity? entity = await passengerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<PassengerEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.PassengerError.NotFound;

        logger.LogNotFound(
            nameof(DeleteCommandHandler),
            nameof(GetByIdAsync), id, error);

        return Result<PassengerEntity>.FromNotFound(error);
    }

    private async Task DeleteAsync(
        PassengerEntity passenger,
        CancellationToken cancellationToken)
    {
        await passengerWriteRepository.DeleteAsync(
            passenger, cancellationToken);

        logger.LogDeleted(nameof(DeleteCommandHandler),
            nameof(Handle), passenger.Id);
    }
}
