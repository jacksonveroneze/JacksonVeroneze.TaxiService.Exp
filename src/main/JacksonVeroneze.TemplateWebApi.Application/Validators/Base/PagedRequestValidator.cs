using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Request.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.Validators.Base;

public class PagedRequestValidator : AbstractValidator<PagedRequest>
{

    public PagedRequestValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Page)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();

        RuleFor(request => request.PageSize)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}
