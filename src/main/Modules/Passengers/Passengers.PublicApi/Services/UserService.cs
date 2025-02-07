using JacksonVeroneze.NET.Result;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;

namespace TaxiService.Modules.Passengers.PublicApi.Services;

public class UserService(
    ILogger<UserService> logger,
    IUserReadRepository userReadRepository) : IUserService
{
    public async Task<Result<bool>> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        UserEntity? entity = await userReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<bool>.WithSuccess(true);
        }

        Error error = DomainErrors.UserError.NotFound;

        logger.LogNotFound(
            nameof(UserService),
            nameof(ExistsByIdAsync), id, error);

        return Result<bool>.FromNotFound(error);
    }
}