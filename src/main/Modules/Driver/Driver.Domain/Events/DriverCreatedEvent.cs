using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Driver.Domain.Events;

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
