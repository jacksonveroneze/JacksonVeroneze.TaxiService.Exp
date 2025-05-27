using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Email.Delete;

public sealed record DeleteEmailCommand(Guid Id, Guid EmailId)
    : IRequest<Result>;
