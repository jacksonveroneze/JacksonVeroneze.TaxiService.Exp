using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.User;

public sealed record ActivateUserCommand(Guid Id) :
    IRequest<IResult<VoidResponse>>;
