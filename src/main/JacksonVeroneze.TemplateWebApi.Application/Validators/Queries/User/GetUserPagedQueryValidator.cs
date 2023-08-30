using JacksonVeroneze.TemplateWebApi.Application.Queries.User;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Queries.User;

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
                .NotEqual(UserStatus.None)
                .IsInEnum();
        });
    }
}
