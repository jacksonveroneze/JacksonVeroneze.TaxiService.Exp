using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Block;

public sealed class BlockDriverCommandHandler(
    ILogger<BlockDriverCommandHandler> logger,
    IDriverRepository driverRepository)
    : IRequestHandler<BlockDriverCommand, Result>
{
    public async Task<Result> Handle(
        BlockDriverCommand request,
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

        Result resultAction = Block(entity, request);

        if (resultAction.IsFailure)
        {
            return resultAction;
        }

        await driverRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(
            nameof(BlockDriverCommandHandler),
            nameof(Handle), entity.Id);

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
}
