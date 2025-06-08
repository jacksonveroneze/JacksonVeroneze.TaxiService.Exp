using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Driver.Domain;
using TaxiService.Modules.Driver.Domain.Core.Errors;
using TaxiService.Modules.Driver.Domain.Repositories;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Inactivate;

public sealed class InactivateDriverCommandHandler(
    ILogger<InactivateDriverCommandHandler> logger,
    IDriverRepository driverRepository,
    IDateTimeProvider dateTime,
    IDomainEventDispatcher eventDispatcher)
    : IRequestHandler<InactivateDriverCommand, Result>
{
    public async Task<Result> Handle(
        InactivateDriverCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Domain.Aggregates.Driver? entity = await driverRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result.FromNotFound(
                DomainErrors.DriverErrors.NotFound);
        }

        Result resultAction = Inactivate(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await driverRepository.UpdateAsync(
            entity, cancellationToken);

        await eventDispatcher.DispatchAsync(entity);

        logger.LogProcessed(
            ModuleConstants.ContextName,
            nameof(InactivateDriverCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Inactivate(Domain.Aggregates.Driver entity)
    {
        Result result = entity.Inactivate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                ModuleConstants.ContextName,
                nameof(InactivateDriverCommandHandler),
                nameof(Inactivate), entity.Id, result.FirstError!);
        }

        return result;
    }
}
