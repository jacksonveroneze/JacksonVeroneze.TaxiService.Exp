using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services;

public interface IActivateUserService
{
    Task<IResult<VoidResponse>> ActivateAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
