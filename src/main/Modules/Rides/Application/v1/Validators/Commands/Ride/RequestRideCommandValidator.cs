using TaxiService.Modules.Rides.Application.v1.Commands.Ride;
using TaxiService.Modules.Rides.Application.v1.Validators.Queries;
using TaxiService.Modules.Rides.Application.Validators.Base;

namespace TaxiService.Modules.Rides.Application.v1.Validators.Commands.Ride;

public class RequestRideCommandValidator : AbstractValidator<RequestRideCommand>
{
    public RequestRideCommandValidator()
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