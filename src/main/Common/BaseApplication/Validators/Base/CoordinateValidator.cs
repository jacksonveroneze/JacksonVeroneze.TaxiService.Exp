namespace TaxiService.Common.Application.Validators.Base;

public class CoordinateValidator : AbstractValidator<float>
{
    public CoordinateValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}