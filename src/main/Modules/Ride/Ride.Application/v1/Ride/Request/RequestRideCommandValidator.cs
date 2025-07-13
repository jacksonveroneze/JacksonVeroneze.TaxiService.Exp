using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Ride.Application.v1.Ride.Request;

public class RequestRideCommandValidator : AbstractValidator<RequestRideCommand>
{
    public RequestRideCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.PassengerId)
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
