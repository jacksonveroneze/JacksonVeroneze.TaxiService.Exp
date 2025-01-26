using AutoMapper;
using JacksonVeroneze.NET.Result;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;
using TaxiService.Modules.Passengers.PublicApi.Models;

namespace TaxiService.Modules.Passengers.PublicApi.Services;

public class UserService(
    ILogger<UserService> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository) : IUserService
{
    public async Task<Result<UserResponse>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        UserEntity? entity = await userReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            UserResponse? response = mapper.Map<UserResponse>(entity);

            return Result<UserResponse>.WithSuccess(response);
        }

        Error error = DomainErrors.UserError.NotFound;

        logger.LogNotFound(
            nameof(UserService),
            nameof(GetByIdAsync), id, error);

        return Result<UserResponse>.FromNotFound(error);
    }
}