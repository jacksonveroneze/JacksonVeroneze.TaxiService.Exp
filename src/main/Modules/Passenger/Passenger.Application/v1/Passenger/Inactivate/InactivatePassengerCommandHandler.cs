using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.SharedKernel.Interfaces;
using TaxiService.Modules.Passenger.Domain;
using TaxiService.Modules.Passenger.Domain.Core.Errors;
using TaxiService.Modules.Passenger.Domain.Repositories;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.Inactivate;

public sealed class InactivatePassengerCommandHandler(
    ILogger<InactivatePassengerCommandHandler> logger,
    IPassengerRepository passengerRepository,
    IDateTimeProvider dateTime,
    IDomainEventPublisher eventPublisher)
    : IRequestHandler<InactivatePassengerCommand, Result>
{
    public async Task<Result> Handle(
        InactivatePassengerCommand request,
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

        Result resultAction = Inactivate(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await passengerRepository.UpdateAsync(
            entity, cancellationToken);

        await eventPublisher.PublishAsync(entity, cancellationToken);

        logger.LogProcessed(
            ModuleConstants.ContextName,
            nameof(InactivatePassengerCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Inactivate(Domain.Aggregates.Passenger entity)
    {
        Result result = entity.Inactivate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogRuleViolation(
                ModuleConstants.ContextName,
                nameof(InactivatePassengerCommandHandler),
                nameof(Inactivate), entity.Id, result.FirstError!);
        }

        return result;
    }
}
