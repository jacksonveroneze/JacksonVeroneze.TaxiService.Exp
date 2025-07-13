using TaxiService.Common.BaseApplication.Models.Common;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Models;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.GetById;

public sealed record GetPassengerByIdQueryResponse
    : DataResponse<PassengerResponse>;
