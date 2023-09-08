using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;

public sealed record DeleteUserCommand(Guid Id) :
    IRequest<IResult<VoidResponse>>;
