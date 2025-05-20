using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Passengers.Application.v1.Commands.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Validators;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Passenger;

public sealed class ActivateCommandHandler(
    ILogger<ActivateCommandHandler> logger,
    IPassengerValidator passengerValidator,
    IPassengerWriteRepository passengerWriteRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<ActivateCommand, Result>
{
    public async Task<Result> Handle(
        ActivateCommand request,
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
                nameof(ActivateCommandHandler),
                nameof(Activate), entity.Id, result.FirstError!);
        }

        return result;
    }

    private async Task UpdateAsync(
        PassengerEntity entity,
        CancellationToken cancellationToken)
    {
        await passengerWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(ActivateCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}
