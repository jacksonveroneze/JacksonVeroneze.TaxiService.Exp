using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Models;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Create;

public sealed record CreatePassengerCommandResponse
    : DataResponse<PassengerResponse>;
