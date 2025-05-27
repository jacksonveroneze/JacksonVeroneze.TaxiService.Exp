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

        PassengerEntity? entity = await passengerRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result.FromNotFound(
                DomainErrors.PassengerError.NotFound);
        }

        await passengerRepository.DeleteAsync(
            entity, cancellationToken);

        logger.LogDeleted(nameof(DeletePassengerCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }
}
