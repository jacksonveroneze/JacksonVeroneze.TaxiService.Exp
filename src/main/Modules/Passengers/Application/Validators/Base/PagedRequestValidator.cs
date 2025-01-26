using TaxiService.Modules.Passengers.Application.Queries.Base;

namespace TaxiService.Modules.Passengers.Application.Validators.Base;

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