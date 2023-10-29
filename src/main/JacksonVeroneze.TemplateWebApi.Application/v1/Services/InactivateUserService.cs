using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.System;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services;

public sealed class InactivateUserService : IInactivateUserService
{
    private readonly ILogger<InactivateUserService> _logger;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;
    private readonly IDateTime _dateTime;

    public InactivateUserService(
        ILogger<InactivateUserService> logger,
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository,
        IDateTime dateTime)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _dateTime = dateTime;
    }

    public async Task<IResult<VoidResponse>> InactivateAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);

        UserEntity? entity = await _readRepository
            .GetByIdAsync(userId, cancellationToken);

        if (entity is null)
        {
            _logger.LogNotFound(nameof(InactivateUserService),
                nameof(InactivateAsync), userId, DomainErrors.User.NotFound);

            return Result<VoidResponse>.NotFound(
                DomainErrors.User.NotFound);
        }

        IResult result = entity.Inactivate(_dateTime.UtcNow);

        if (result.IsFailure)
        {
            _logger.LogAlreadyProcessed(nameof(InactivateUserService),
                nameof(InactivateAsync), result.Error!, userId);

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(entity, cancellationToken);

        _logger.LogProcessed(nameof(InactivateUserService),
            nameof(InactivateAsync), userId);

        return Result<VoidResponse>.Success();
    }
}
