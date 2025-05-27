using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Models.Passenger;

public sealed record CreatePassengerCommandResponse
    : DataResponse<PassengerResponse>;
