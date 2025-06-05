using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.GetById;

public sealed record GetPassengerByIdQuery(Guid Id) :
    IRequest<Result<GetPassengerByIdQueryResponse>>;
