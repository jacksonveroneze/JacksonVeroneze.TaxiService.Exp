using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.User;

public interface IDeleteUserService
{
    Task<Result> DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
