using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.System;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services;

public sealed class ActivateUserService : IActivateUserService
{
    private readonly ILogger<ActivateUserService> _logger;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;
    private readonly IIntegrationEventPublisher _eventPublisher;
    private readonly IDateTime _dateTime;

    public ActivateUserService(
        ILogger<ActivateUserService> logger,
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository,
        IIntegrationEventPublisher eventPublisher,
        IDateTime dateTime)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _eventPublisher = eventPublisher;
        _dateTime = dateTime;
    }

    public async Task<IResult<VoidResponse>> ActivateAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);

        UserEntity? entity = await _readRepository
            .GetByIdAsync(userId, cancellationToken);

        if (entity is null)
        {
            _logger.LogNotFound(nameof(ActivateUserService),
                nameof(ActivateAsync), userId,
                DomainErrors.User.NotFound);

            return Result<VoidResponse>.NotFound(
                DomainErrors.User.NotFound);
        }

        IResult result = entity.Activate(_dateTime.UtcNow);

        if (result.IsFailure)
        {
            _logger.LogAlreadyProcessed(nameof(ActivateUserService),
                nameof(ActivateAsync), result.Error!, userId);

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(
            entity, cancellationToken);

        IEnumerable<Task>? tasks = entity.Events?
            .Select(evt => _eventPublisher.PublishAsync(
                evt, cancellationToken));

        await Task.WhenAll(tasks!);

        entity.ClearEvents();

        _logger.LogProcessed(nameof(ActivateUserService),
            nameof(ActivateAsync), userId);

        return Result<VoidResponse>.Success();
    }
}
