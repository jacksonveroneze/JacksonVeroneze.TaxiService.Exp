using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
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

        PassengerEntity? entity = await passengerRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result.FromNotFound(
                DomainErrors.PassengerError.NotFound);
        }

        Result resultAction = Inactivate(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await passengerRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(InactivatePassengerCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Inactivate(PassengerEntity entity)
    {
        Result result = entity.Inactivate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogRuleViolation(
                nameof(InactivatePassengerCommandHandler),
                nameof(Inactivate), entity.Id, result.FirstError!);
        }

        return result;
    }
}
