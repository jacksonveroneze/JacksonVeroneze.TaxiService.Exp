using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.GetById;

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
