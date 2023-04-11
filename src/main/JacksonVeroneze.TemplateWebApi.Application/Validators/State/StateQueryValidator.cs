namespace JacksonVeroneze.TemplateWebApi.Application.Validators.State;

public class StateQueryValidator : AbstractValidator<string>
{
    public StateQueryValidator()
    {
        RuleFor(request => request)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .Length(2, 2)
            .Matches(@"[a-zA-Z]{2,2}");
    }
}