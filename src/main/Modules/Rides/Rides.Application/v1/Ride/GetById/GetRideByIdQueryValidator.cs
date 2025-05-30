using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Rides.Application.v1.Ride.GetById;

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
