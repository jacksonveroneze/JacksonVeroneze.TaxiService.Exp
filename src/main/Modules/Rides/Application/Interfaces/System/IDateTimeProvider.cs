namespace TaxiService.Modules.Rides.Application.Interfaces.System;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }

    public DateTime Now { get; }

    public DateOnly DateNow { get; }

    public TimeOnly TimeNow { get; }
}