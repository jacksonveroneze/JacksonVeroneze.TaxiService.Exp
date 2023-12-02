namespace JacksonVeroneze.TaxiService.Exp.Application.Interfaces.System;

public interface IDateTime
{
    DateTime UtcNow { get; }

    DateTime Now { get; }

    DateOnly DateNow { get; }

    TimeOnly TimeNow { get; }
}
