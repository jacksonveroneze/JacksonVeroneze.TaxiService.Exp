using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Queries.User;

public class GetUserPagedQueryValidator : AbstractValidator<GetUserPagedQuery>
{
    private const int MinLengthName = 2;

    public GetUserPagedQueryValidator()
    {
        RuleFor(request => request)
            .SetValidator(new PagedRequestValidator());

        When(request => !string.IsNullOrEmpty(request.Name), () =>
        {
            RuleFor(request => request.Name)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(MinLengthName);
        });

        When(request => request.Status != null, () =>
        {
            RuleFor(request => request.Status)
                .Cascade(CascadeMode.Stop)
                .IsInEnum()
                .NotEqual(UserStatus.None);
        });
    }
}
