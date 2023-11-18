using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.System;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services.User;

public sealed class InactivateUserService : IInactivateUserService
{
    private readonly ILogger<InactivateUserService> _logger;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;
    private readonly IIntegrationEventPublisher _eventPublisher;
    private readonly IDateTime _dateTime;

    public InactivateUserService(
        ILogger<InactivateUserService> logger,
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
                nameof(InactivateAsync), userId, result.Error!);

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(
            entity, cancellationToken);

        await _eventPublisher.PublishAsync(
            new UserInactivatedDomainEvent(entity.Id), cancellationToken);

        // IEnumerable<Task>? tasks = entity.Events?
        //     .Select(evt => _eventPublisher.PublishAsync(
        //         evt, cancellationToken));
        //
        // await Task.WhenAll(tasks!);
        //
        // entity.ClearEvents();

        _logger.LogProcessed(nameof(InactivateUserService),
            nameof(InactivateAsync), userId);

        return Result<VoidResponse>.Success();
    }
}
