using TaxiService.Common.BaseApplication.Validators.Base;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passenger;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Queries.Passenger;

public class GetPassengerByIdQueryValidator : AbstractValidator<GetPassengerByIdQuery>
{
    public GetPassengerByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
