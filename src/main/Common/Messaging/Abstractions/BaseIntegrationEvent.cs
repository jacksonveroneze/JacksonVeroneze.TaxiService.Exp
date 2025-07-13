namespace TaxiService.Common.Messaging.Abstractions;

public abstract record BaseIntegrationEvent(
    string? ModuleName, string? EvtName);
