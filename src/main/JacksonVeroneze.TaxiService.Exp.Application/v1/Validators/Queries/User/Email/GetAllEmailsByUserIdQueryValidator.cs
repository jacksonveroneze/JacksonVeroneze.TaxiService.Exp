using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Queries.User.Email;

public class GetAllEmailsByUserIdQueryValidator : AbstractValidator<GetEmailsByUserIdPagedQuery>
{
    public GetAllEmailsByUserIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.UserId)
            .SetValidator(new IdGuidValidator());
    }
}