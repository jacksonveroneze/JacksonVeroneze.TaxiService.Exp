using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services;

public interface IInactivateUserService
{
    Task<IResult<VoidResponse>> InactivateAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}