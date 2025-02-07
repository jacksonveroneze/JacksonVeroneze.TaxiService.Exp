using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;

namespace TaxiService.Modules.Rides.Application.v1.Validators.Commands.Ride;

public class StartCommandValidator : AbstractValidator<StartCommand>
{
    public StartCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}