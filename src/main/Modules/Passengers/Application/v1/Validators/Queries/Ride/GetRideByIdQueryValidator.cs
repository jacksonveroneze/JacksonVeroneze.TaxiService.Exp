using TaxiService.Modules.Passengers.Application.v1.Queries.Ride;
using TaxiService.Modules.Passengers.Application.Validators.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Queries.Ride;

public class GetRideByIdQueryValidator : AbstractValidator<GetRideByIdQuery>
{
    public GetRideByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}