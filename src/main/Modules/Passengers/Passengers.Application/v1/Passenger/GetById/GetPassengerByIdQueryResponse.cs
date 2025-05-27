using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.GetById;

public sealed record GetPassengerByIdQueryResponse
    : DataResponse<PassengerResponse>;
