using JacksonVeroneze.TaxiService.Exp.Application.Queries.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.Validators.Base;

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