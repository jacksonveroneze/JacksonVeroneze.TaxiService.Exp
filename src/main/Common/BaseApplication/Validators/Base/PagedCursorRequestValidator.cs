using TaxiService.Common.BaseApplication.Queries.Base;

namespace TaxiService.Common.BaseApplication.Validators.Base;

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