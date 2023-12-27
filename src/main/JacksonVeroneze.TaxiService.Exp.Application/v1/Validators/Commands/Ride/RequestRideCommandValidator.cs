using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Commands.Ride;

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