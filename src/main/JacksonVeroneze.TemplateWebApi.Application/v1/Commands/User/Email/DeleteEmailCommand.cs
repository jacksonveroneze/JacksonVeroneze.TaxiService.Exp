using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;

public sealed record DeleteEmailCommand(Guid Id, Guid EmailId) :
    IRequest<IResult<VoidResponse>>;
