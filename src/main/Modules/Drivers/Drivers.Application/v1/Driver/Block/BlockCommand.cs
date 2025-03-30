using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Block;

public sealed record BlockCommand(
    Guid Id, BlockCommand.BlockEnum Type) : IRequest<Result>
{
    public enum BlockEnum
    {
        None,
        TemporaryBlock,
        PermanentBlock
    }
}