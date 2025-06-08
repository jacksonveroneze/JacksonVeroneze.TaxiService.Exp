using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Driver.Application.v1.Driver.GetById;

public class GetDriverByIdQueryValidator
    : AbstractValidator<GetDriverByIdQuery>
{
    public GetDriverByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
