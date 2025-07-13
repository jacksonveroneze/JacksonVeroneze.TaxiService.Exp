namespace TaxiService.Common.BaseApplication.Validators;

public class IdGuidValidator : AbstractValidator<Guid>
{
    public IdGuidValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}
