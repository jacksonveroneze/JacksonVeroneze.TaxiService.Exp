using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;

public sealed record DeleteEmailCommand :
    IRequest<IResult<VoidResponse>>
{
    [FromRoute(Name = "userId")]
    public Guid Id { get; init; }

    [FromRoute(Name = "emailId")]
    public Guid EmailId { get; init; }
}
