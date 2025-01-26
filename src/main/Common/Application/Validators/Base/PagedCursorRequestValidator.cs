using TaxiService.Common.Application.Queries.Base;

namespace TaxiService.Common.Application.Validators.Base;

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