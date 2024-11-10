using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Validators.Base;

public class PagedCursorRequestValidator : AbstractValidator<PagedCursorQuery>
{
    public PagedCursorRequestValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.PageSize)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(0);
    }
}