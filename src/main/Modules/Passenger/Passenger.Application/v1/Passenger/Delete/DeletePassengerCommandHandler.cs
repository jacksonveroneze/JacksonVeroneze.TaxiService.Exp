using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Modules.Passenger.Domain;
using TaxiService.Modules.Passenger.Domain.Core.Errors;
using TaxiService.Modules.Passenger.Domain.Repositories;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.Delete;

public sealed class DeletePassengerCommandHandler(
    ILogger<DeletePassengerCommandHandler> logger,
    IPassengerRepository passengerRepository,
    IDomainEventDispatcher eventDispatcher)
    : IRequestHandler<DeletePassengerCommand, Result>
{
    public async Task<Result> Handle(
        DeletePassengerCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Domain.Aggregates.Passenger? entity = await passengerRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result.FromNotFound(
                DomainErrors.PassengerError.NotFound);
        }

        await passengerRepository.DeleteAsync(
            entity, cancellationToken);

        await eventDispatcher.DispatchAsync(entity, cancellationToken);

        logger.LogDeleted(
            ModuleConstants.ContextName,
            nameof(DeletePassengerCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }
}
