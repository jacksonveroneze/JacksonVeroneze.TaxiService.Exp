using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Models.User.Email;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.User.Email;

public sealed record GetAllEmailsByUserIdQuery(Guid Id) : IRequest<IResult<GetAllEmailsByUserIdQueryResponse>>;
