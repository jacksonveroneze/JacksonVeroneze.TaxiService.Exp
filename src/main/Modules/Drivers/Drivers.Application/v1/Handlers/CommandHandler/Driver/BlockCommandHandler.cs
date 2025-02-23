using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.System;
using TaxiService.Modules.Drivers.Application.v1.Commands.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.Driver;

public sealed class BlockCommandHandler(
    ILogger<BlockCommandHandler> logger,
    IDriverService driverService,
    IDriverWriteRepository driverWriteRepository,
    IDateTimeProvider dateTime)
    : IRequestHandler<BlockCommand, Result>
{
    public async Task<Result> Handle(
        BlockCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<DriverEntity> resultEntity =
            await driverService.GetByIdAsync(
                request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        Result resultAction = Block(resultEntity.Value!, request);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await UpdateAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private Result Block(DriverEntity entity, BlockCommand request)
    {
        var result = request.Type switch
        {
            BlockCommand.BlockEnum.TemporaryBlock =>
                entity.BlockTemporarily(dateTime.UtcNow),

            BlockCommand.BlockEnum.PermanentBlock =>
                entity.BlockPermanently(dateTime.UtcNow),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(
                nameof(BlockCommandHandler),
                nameof(Block), entity.Id, result.Error!);
        }

        return result;
    }

    private async Task UpdateAsync(
        DriverEntity entity,
        CancellationToken cancellationToken)
    {
        await driverWriteRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(BlockCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}