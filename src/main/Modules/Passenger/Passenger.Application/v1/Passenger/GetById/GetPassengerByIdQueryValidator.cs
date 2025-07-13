using TaxiService.Common.BaseApplication.Validators;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.GetById;

public class GetPassengerByIdQueryValidator
    : AbstractValidator<GetPassengerByIdQuery>
{
    public GetPassengerByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
