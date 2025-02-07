using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;

namespace TaxiService.Modules.Rides.Application.v1.Validators.Commands.Ride;

public class AcceptCommandValidator : AbstractValidator<AcceptCommand>
{
    public AcceptCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.Body)
            .NotNull();

        RuleFor(request => request.Body!.DriverId)
            .SetValidator(new IdGuidValidator());
    }
}