using TaxiService.Common.BaseApplication.Queries.Base;

namespace TaxiService.Common.BaseApplication.Validators.Base;

public class PagedOffsetRequestValidator : AbstractValidator<PagedOffsetQuery>
{
    public PagedOffsetRequestValidator()
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