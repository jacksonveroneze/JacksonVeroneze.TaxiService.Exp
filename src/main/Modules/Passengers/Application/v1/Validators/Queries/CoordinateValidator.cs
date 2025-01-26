namespace TaxiService.Modules.Passengers.Application.v1.Validators.Queries;

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