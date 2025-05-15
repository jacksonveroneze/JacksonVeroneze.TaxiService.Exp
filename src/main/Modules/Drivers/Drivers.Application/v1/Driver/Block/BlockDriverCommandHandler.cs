using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Block;

public sealed class BlockDriverCommandHandler(
    ILogger<BlockDriverCommandHandler> logger,
    IDriverService driverService,
    IDriverRepository driverRepository)
    : IRequestHandler<BlockDriverCommand, Result>
{
    public async Task<Result> Handle(
        BlockDriverCommand request,
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

    private Result Block(
        DriverEntity entity, 
        BlockDriverCommand request)
    {
        Result result = request.Type switch
        {
            BlockDriverCommand.BlockType.TemporaryBlock =>
                entity.BlockTemporarily(),

            BlockDriverCommand.BlockType.PermanentBlock =>
                entity.BlockPermanently(),

            _ => throw new ArgumentOutOfRangeException(
                nameof(request), request.Type, "Tipo inválido."),
        };

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(BlockDriverCommandHandler),
                nameof(Block),
                entity.Id,
                result.FirstError!);
        }

        return result;
    }


    private async Task UpdateAsync(
        DriverEntity entity,
        CancellationToken cancellationToken)
    {
        await driverRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(BlockDriverCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}
