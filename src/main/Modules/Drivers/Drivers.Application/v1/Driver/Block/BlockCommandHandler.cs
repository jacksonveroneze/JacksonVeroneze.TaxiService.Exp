using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Block;

public sealed class BlockCommandHandler(
    ILogger<BlockCommandHandler> logger,
    IDriverService driverService,
    IDriverRepository driverRepository)
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
        Result result = request.Type switch
        {
            BlockCommand.BlockType.TemporaryBlock =>
                entity.BlockTemporarily(),

            BlockCommand.BlockType.PermanentBlock =>
                entity.BlockPermanently(),

            _ => throw new ArgumentOutOfRangeException(
                nameof(request), request.Type, "Tipo inválido."),
        };

        if (result.IsFailure)
        {
            logger.LogGenericValidationError(
                nameof(BlockCommandHandler),
                nameof(Block),
                entity.Id,
                result.Error!);
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
            nameof(BlockCommandHandler),
            nameof(UpdateAsync), entity.Id);
    }
}
