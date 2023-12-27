using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;

public sealed record GetAllEmailsByUserIdQuery(Guid Id) :
    IRequest<Result<GetAllEmailsByUserIdQueryResponse>>;