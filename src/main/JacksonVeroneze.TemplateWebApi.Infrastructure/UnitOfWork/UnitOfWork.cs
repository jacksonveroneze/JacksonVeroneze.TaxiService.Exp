using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.UnitOfWork;

[ExcludeFromCodeCoverage]
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IIntegrationEventPublisher _bus;

    public UnitOfWork(ApplicationDbContext dbContext,
        IIntegrationEventPublisher bus)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    public async Task<bool> CommitAsync(
        CancellationToken cancellationToken)
    {
        bool isSuccess = await _dbContext
            .SaveChangesAsync(cancellationToken) > 0;

        if (!isSuccess)
        {
            throw new InvalidOperationException();
        }

        await PublishDomainEvents(_dbContext,
            cancellationToken);

        return true;
    }

    private async Task PublishDomainEvents(
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        IList<EntityEntry<BaseEntityAggregateRoot>> aggregateRoots =
            dbContext.ChangeTracker
                .Entries<BaseEntityAggregateRoot>()
                .Where(item => item.Entity.Events != null
                               && item.Entity.Events.Any())
                .ToList();

        if (!aggregateRoots.Any())
        {
            return;
        }

        List<DomainEvent> domainEvents = aggregateRoots
            .SelectMany(entityEntry => entityEntry.Entity.Events!)
            .ToList();

        aggregateRoots.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        IEnumerable<Task> tasks = domainEvents
            .Select(evt => _bus.PublishAsync(evt, cancellationToken));

        await Task.WhenAll(tasks);
    }
}
