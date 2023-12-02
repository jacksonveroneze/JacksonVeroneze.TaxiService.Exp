using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Base;

public class PagedRequestValidator : AbstractValidator<PagedQuery>
{
    public PagedRequestValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Page)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(0);

        RuleFor(request => request.PageSize)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(0);
    }
}
