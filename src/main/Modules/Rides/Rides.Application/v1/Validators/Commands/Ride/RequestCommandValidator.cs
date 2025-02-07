using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;

namespace TaxiService.Modules.Rides.Application.v1.Validators.Commands.Ride;

public class RequestCommandValidator : AbstractValidator<RequestCommand>
{
    public RequestCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.UserId)
            .SetValidator(new IdGuidValidator());

        RuleFor(request => request.LatitudeFrom)
            .SetValidator(new CoordinateValidator());

        RuleFor(request => request.LongitudeFrom)
            .SetValidator(new CoordinateValidator());

        RuleFor(request => request.LatitudeTo)
            .SetValidator(new CoordinateValidator());

        RuleFor(request => request.LongitudeTo)
            .SetValidator(new CoordinateValidator());
    }
}