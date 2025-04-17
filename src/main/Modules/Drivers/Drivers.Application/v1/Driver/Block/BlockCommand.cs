using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Block;

public sealed record BlockCommand(
    Guid Id, BlockCommand.BlockType Type) : IRequest<Result>
{
    public enum BlockType
    {
        None,
        TemporaryBlock,
        PermanentBlock,
    }
}