using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Driver.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class DriverCreatedEvent(
    Guid aggregateId,
    string name,
    string email,
    string cpf)
    : BaseDomainEvent(aggregateId, nameof(DriverCreatedEvent))
{
    public string Name { get; } = name;

    public string Email { get; } = email;

    public string Cpf { get; } = cpf;
}
