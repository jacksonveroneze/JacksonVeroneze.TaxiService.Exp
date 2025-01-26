using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;

public sealed record GetUserByIdQuery(Guid Id) :
    IRequest<Result<GetUserByIdQueryResponse>>;