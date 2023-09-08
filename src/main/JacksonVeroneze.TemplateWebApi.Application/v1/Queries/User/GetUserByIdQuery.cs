using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User;

public sealed record GetUserByIdQuery(Guid Id) :
    IRequest<IResult<GetUserByIdQueryResponse>>;
