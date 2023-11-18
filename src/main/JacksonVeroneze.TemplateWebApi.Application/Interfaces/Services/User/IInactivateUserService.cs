using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;

public interface IInactivateUserService
{
    Task<IResult> InactivateAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
