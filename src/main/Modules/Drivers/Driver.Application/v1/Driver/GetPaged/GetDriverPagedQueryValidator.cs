namespace TaxiService.Modules.Driver.Application.v1.Driver.GetPaged;

public class GetDriverPagedQueryValidator
    : AbstractValidator<GetDriverPagedQuery>
{
    public GetDriverPagedQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();
    }
}
