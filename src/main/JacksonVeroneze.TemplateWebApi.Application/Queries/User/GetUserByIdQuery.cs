using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.User;

public sealed record GetUserByIdQuery(Guid Id) :
    IRequest<IResult<GetUserByIdQueryResponse>>;
