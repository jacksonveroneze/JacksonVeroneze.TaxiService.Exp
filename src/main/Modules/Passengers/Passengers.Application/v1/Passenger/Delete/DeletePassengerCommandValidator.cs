using TaxiService.Common.BaseApplication.Validators.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Delete;

public class DeletePassengerCommandValidator 
    : AbstractValidator<DeletePassengerCommand>
{
    public DeletePassengerCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
