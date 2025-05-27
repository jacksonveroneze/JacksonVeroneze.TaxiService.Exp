using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
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

        PassengerEntity? entity = await passengerRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result.FromNotFound(
                DomainErrors.PassengerError.NotFound);
        }

        Result resultAction = Activate(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await passengerRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(ActivatePassengerCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Activate(PassengerEntity entity)
    {
        Result result = entity.Activate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogRuleViolation(
                nameof(ActivatePassengerCommandHandler),
                nameof(Activate), entity.Id, result.FirstError!);
        }

        return result;
    }
}
