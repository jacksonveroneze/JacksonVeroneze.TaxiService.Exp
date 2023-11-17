using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services.User;

public sealed class GetUserService : IGetUserService
{
    private readonly ILogger<GetUserService> _logger;
    private readonly IUserReadRepository _readRepository;

    public GetUserService(
        ILogger<GetUserService> logger,
        IUserReadRepository readRepository)
    {
        _logger = logger;
        _readRepository = readRepository;
    }

    public async Task<IResult<UserEntity>> TryGetUserAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        UserEntity? entity = await _readRepository
            .GetByIdAsync(id, cancellationToken);

        return entity is not null
            ? Result<UserEntity>.Success(entity)
            : Result<UserEntity>.NotFound(DomainErrors.Ride.NotFound);
    }
}
