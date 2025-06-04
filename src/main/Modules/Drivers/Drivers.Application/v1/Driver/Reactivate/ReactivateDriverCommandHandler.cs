using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Repositories;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Reactivate;

public sealed class ReactivateDriverCommandHandler(
    ILogger<ReactivateDriverCommandHandler> logger,
    IDriverRepository driverRepository,
    IDomainEventDispatcher eventDispatcher)
    : IRequestHandler<ReactivateDriverCommand, Result>
{
    public async Task<Result> Handle(
        ReactivateDriverCommand request,
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

        Result resultAction = Reactivate(entity);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await driverRepository.UpdateAsync(
            entity, cancellationToken);

        await eventDispatcher.DispatchAsync(entity);

        logger.LogProcessed(
            nameof(ReactivateDriverCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Reactivate(Domain.Aggregates.Driver entity)
    {
        Result result = entity.Reactivate();

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(ReactivateDriverCommandHandler),
                nameof(Reactivate), entity.Id, result.FirstError!);
        }

        return result;
    }
}
