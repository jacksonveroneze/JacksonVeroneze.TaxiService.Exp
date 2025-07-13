using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.SharedKernel.Interfaces;
using TaxiService.Modules.Passenger.Domain;
using TaxiService.Modules.Passenger.Domain.Core.Errors;
using TaxiService.Modules.Passenger.Domain.Repositories;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.Activate;

public sealed class ActivatePassengerCommandHandler(
    ILogger<ActivatePassengerCommandHandler> logger,
    IPassengerRepository passengerRepository,
    IDateTimeProvider dateTime,
    IDomainEventPublisher eventPublisher)
    : IRequestHandler<ActivatePassengerCommand, Result>
{
    public async Task<Result> Handle(
        ActivatePassengerCommand request,
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

        Result resultAction = Activate(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await passengerRepository.UpdateAsync(
            entity, cancellationToken);

        await eventPublisher.PublishAsync(entity, cancellationToken);

        logger.LogProcessed(
            ModuleConstants.ContextName,
            nameof(ActivatePassengerCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Activate(Domain.Aggregates.Passenger entity)
    {
        Result result = entity.Activate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogRuleViolation(
                ModuleConstants.ContextName,
                nameof(ActivatePassengerCommandHandler),
                nameof(Activate), entity.Id, result.FirstError!);
        }

        return result;
    }
}
