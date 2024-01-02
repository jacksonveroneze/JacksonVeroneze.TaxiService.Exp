using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Messaging;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Contexts;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.UnitOfWork;

[ExcludeFromCodeCoverage]
public class EfCoreUnitOfWork(
    ApplicationDbContext dbContext,
    IIntegrationEventPublisher bus)
    : IUnitOfWork
{
    public async Task<bool> CommitAsync(
        CancellationToken cancellationToken)
    {
        bool isSuccess = await dbContext
            .SaveChangesAsync(cancellationToken) > 0;

        if (!isSuccess)
        {
            throw new InvalidOperationException();
        }

        await PublishDomainEvents(dbContext,
            cancellationToken);

        return true;
    }

    private Task PublishDomainEvents(
        ApplicationDbContext applicationDbContext,
        CancellationToken cancellationToken)
    {
        IList<EntityEntry<BaseEntityAggregateRoot>> aggregateRoots =
            applicationDbContext.ChangeTracker
                .Entries<BaseEntityAggregateRoot>()
                .Where(item => item.Entity.Events != null
                               && item.Entity.Events.Any())
                .ToList();

        if (!aggregateRoots.Any())
        {
            return Task.CompletedTask;
        }

        List<DomainEvent> domainEvents = aggregateRoots
            .SelectMany(entityEntry => entityEntry.Entity.Events!)
            .ToList();

        aggregateRoots.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        IEnumerable<Task> tasks = domainEvents
            .Select(evt => bus.PublishAsync(evt, cancellationToken));

        return Task.WhenAll(tasks);
    }
}