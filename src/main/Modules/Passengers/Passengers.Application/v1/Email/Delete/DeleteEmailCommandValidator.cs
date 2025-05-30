using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Email.Delete;

public class DeleteEmailCommandValidator 
    : AbstractValidator<DeleteEmailCommand>
{
    public DeleteEmailCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.PassengerId)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.EmailId)
            .SetValidator(new IdGuidValidator());
    }
}
