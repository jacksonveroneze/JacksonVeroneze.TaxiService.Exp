using TaxiService.Modules.Passengers.Application.v1.Queries.User;
using TaxiService.Modules.Passengers.Application.Validators.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Validators.Queries.User;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}