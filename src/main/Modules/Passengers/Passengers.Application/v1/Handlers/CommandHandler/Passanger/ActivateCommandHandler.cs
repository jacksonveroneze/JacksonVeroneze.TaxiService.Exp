using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Passengers.Application.v1.Commands.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Validators;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Passanger;

public sealed class ActivateCommandHandler(
    ILogger<ActivateCommandHandler> logger,
    IPassengerValidator passengerValidator,
    IPassangerWriteRepository passangerWriteRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<ActivateCommand, Result>
{
    public async Task<Result> Handle(
        ActivateCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassangerEntity> entity = await passengerValidator
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

    private Result Activate(PassangerEntity entity)
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
        PassangerEntity entity,
        CancellationToken cancellationToken)
    {
        await passangerWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(ActivateCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}
