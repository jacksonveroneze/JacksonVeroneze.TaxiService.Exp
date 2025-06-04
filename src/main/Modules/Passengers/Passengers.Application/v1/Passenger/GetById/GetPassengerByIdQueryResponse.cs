using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Models;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.GetById;

public sealed record GetPassengerByIdQueryResponse
    : DataResponse<PassengerResponse>;
