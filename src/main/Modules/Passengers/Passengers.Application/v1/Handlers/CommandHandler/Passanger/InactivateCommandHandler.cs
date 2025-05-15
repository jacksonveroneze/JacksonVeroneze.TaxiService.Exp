using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Passengers.Application.v1.Commands.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Validators;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Passanger;

public sealed class InactivateCommandHandler(
    ILogger<InactivateCommandHandler> logger,
    IPassengerValidator passengerValidator,
    IPassangerWriteRepository passangerWriteRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<InactivateCommand, Result>
{
    public async Task<Result> Handle(
        InactivateCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassangerEntity> entity = await passengerValidator
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

    private Result Inactivate(PassangerEntity entity)
    {
        Result result = entity.Inactivate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(
                nameof(InactivateCommandHandler),
                nameof(Inactivate), entity.Id, result.FirstError!);
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
            nameof(InactivateCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}
