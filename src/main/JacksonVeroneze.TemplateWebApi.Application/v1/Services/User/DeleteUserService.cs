using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services.User;

public sealed class DeleteUserService : IDeleteUserService
{
    private readonly ILogger<DeleteUserService> _logger;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public DeleteUserService(
        ILogger<DeleteUserService> logger,
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult> DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);

        UserEntity? entity = await _readRepository
            .GetByIdAsync(userId, cancellationToken);

        if (entity is null)
        {
            return Result.Invalid(
                DomainErrors.User.NotFound);
        }

        await _writeRepository.DeleteAsync(
            entity, cancellationToken);

        _logger.LogDeleted(nameof(DeleteUserService),
            nameof(DeleteAsync), userId);

        return Result.Success();
    }
}
