using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services;

public interface IDeleteUserService
{
    Task<IResult<VoidResponse>> DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
