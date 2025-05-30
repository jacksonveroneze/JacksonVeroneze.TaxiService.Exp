using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Email.Delete;

public sealed record DeleteEmailCommand(Guid PassengerId, Guid EmailId)
    : IRequest<Result>;
