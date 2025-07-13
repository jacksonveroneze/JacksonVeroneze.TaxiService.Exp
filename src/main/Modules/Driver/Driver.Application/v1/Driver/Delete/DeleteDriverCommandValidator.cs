using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Delete;

public class DeleteDriverCommandValidator
    : AbstractValidator<DeleteDriverCommand>
{
    public DeleteDriverCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
