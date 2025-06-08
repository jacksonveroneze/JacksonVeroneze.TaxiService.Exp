using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Driver.Domain;
using TaxiService.Modules.Driver.Domain.Core.Errors;
using TaxiService.Modules.Driver.Domain.Repositories;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Activate;

public sealed class ActivateDriverCommandHandler(
    ILogger<ActivateDriverCommandHandler> logger,
    IDriverRepository driverRepository,
    IDateTimeProvider dateTime,
    IDomainEventDispatcher eventDispatcher)
    : IRequestHandler<ActivateDriverCommand, Result>
{
    public async Task<Result> Handle(
        ActivateDriverCommand request,
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

        Result resultAction = Activate(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await driverRepository.UpdateAsync(
            entity, cancellationToken);

        await eventDispatcher.DispatchAsync(entity);

        logger.LogProcessed(
            ModuleConstants.ContextName,
            nameof(ActivateDriverCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Activate(Domain.Aggregates.Driver entity)
    {
        Result result = entity.Activate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                ModuleConstants.ContextName,
                nameof(ActivateDriverCommandHandler),
                nameof(Activate), entity.Id, result.FirstError!);
        }

        return result;
    }
}
