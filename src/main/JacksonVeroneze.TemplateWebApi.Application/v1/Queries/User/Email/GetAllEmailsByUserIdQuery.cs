using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User.Email;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User.Email;

public sealed record GetAllEmailsByUserIdQuery(Guid Id) : IRequest<IResult<GetAllEmailsByUserIdQueryResponse>>;
