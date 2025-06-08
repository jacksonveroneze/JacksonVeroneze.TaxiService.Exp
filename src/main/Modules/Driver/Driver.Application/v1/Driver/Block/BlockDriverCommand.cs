using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Block;

public sealed record BlockDriverCommand(
    Guid Id,
    BlockDriverCommand.BlockType Type) : IRequest<Result>
{
    public enum BlockType
    {
        None,
        TemporaryBlock,
        PermanentBlock,
    }
}
