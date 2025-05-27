using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Activate;

public sealed class ActivatePassengerCommandHandler(
    ILogger<ActivatePassengerCommandHandler> logger,
    IPassengerRepository passengerRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<ActivatePassengerCommand, Result>
{
    public async Task<Result> Handle(
        ActivatePassengerCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassengerEntity> entity = await passengerValidator
            .ValidateAndGetAsync(request.Id, cancellationToken);

        if (entity.IsFailure)
        {
            return entity;
        }

        Result resultAction = Activate(entity.Value!);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(entity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result Activate(PassengerEntity entity)
    {
        Result result = entity.Activate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(
                nameof(ActivatePassengerCommandHandler),
                nameof(Activate), entity.Id, result.FirstError!);
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
            nameof(ActivatePassengerCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}
