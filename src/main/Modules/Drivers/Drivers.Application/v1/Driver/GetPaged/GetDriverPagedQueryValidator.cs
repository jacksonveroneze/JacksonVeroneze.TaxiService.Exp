namespace TaxiService.Modules.Drivers.Application.v1.Driver.GetPaged;

public class GetDriverPagedQueryValidator
    : AbstractValidator<GetDriverPagedQuery>
{
    public GetDriverPagedQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();
    }
}
