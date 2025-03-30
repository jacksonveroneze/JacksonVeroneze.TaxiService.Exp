using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Block;

public class BlockCommandValidator : AbstractValidator<BlockCommand>
{
    public BlockCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.Type)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEqual(BlockCommand.BlockEnum.None)
            .IsInEnum();
    }
}