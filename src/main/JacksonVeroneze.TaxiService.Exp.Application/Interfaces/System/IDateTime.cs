namespace JacksonVeroneze.TaxiService.Exp.Application.Interfaces.System;

public interface IDateTime
{
    public DateTime UtcNow { get; }

    public DateTime Now { get; }

    public DateOnly DateNow { get; }

    public TimeOnly TimeNow { get; }
}