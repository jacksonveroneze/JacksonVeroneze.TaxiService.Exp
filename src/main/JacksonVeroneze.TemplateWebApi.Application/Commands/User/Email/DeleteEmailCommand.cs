using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.User.Email;

public sealed record DeleteEmailCommand(Guid Id, Guid EmailId) :
    IRequest<IResult<VoidResponse>>;
