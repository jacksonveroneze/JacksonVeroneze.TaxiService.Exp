using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Inactivate;

public sealed class InactivateDriverCommandHandler(
    ILogger<InactivateDriverCommandHandler> logger,
    IDriverRepository driverRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<InactivateDriverCommand, Result>
{
    public async Task<Result> Handle(
        InactivateDriverCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        DriverEntity? entity = await driverRepository
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

        logger.LogProcessed(
            nameof(InactivateDriverCommandHandler),
            nameof(Handle), entity.Id);

        return Result.WithSuccess();
    }

    private Result Inactivate(DriverEntity entity)
    {
        Result result = entity.Inactivate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(InactivateDriverCommandHandler),
                nameof(Inactivate), entity.Id, result.FirstError!);
        }

        return result;
    }
}
