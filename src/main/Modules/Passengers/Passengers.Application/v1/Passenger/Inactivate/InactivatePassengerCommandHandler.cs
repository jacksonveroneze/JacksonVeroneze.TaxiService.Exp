using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Inactivate;

public sealed class InactivatePassengerCommandHandler(
    ILogger<InactivatePassengerCommandHandler> logger,
    IPassengerRepository passengerRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<InactivatePassengerCommand, Result>
{
    public async Task<Result> Handle(
        InactivatePassengerCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassengerEntity> entity = await passengerValidator
            .ValidateAndGetAsync(request.Id, cancellationToken);

        if (entity.IsFailure)
        {
            return entity;
        }

        Result resultAction = Inactivate(entity.Value!);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(entity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result Inactivate(PassengerEntity entity)
    {
        Result result = entity.Inactivate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(
                nameof(InactivatePassengerCommandHandler),
                nameof(Inactivate), entity.Id, result.FirstError!);
        }

        return result;
    }

    private async Task UpdateAsync(
        PassengerEntity entity,
        CancellationToken cancellationToken)
    {
        await passengerRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(InactivatePassengerCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}
