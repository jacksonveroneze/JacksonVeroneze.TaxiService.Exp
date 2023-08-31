using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.User;

public sealed record InactivateUserCommand(Guid Id) :
    IRequest<IResult<VoidResponse>>;
