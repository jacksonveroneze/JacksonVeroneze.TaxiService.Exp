using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Queries.Ride;

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
