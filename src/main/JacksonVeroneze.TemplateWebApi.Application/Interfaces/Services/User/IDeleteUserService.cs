using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;

public interface IDeleteUserService
{
    Task<IResult> DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
