using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.GetById;

public sealed record GetPassengerByIdQuery(Guid Id) :
    IRequest<Result<GetPassengerByIdQueryResponse>>;
