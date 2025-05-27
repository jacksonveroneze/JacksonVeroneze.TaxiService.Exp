using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Create;

public sealed record CreatePassengerCommandResponse
    : DataResponse<PassengerResponse>;
